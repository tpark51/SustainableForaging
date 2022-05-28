using SustainableForaging.Core.Models;
using SustainableForaging.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SustainableForaging.BLL
{
    public class ItemService
    {
        private readonly IItemRepository repository;

        public ItemService(IItemRepository repository)
        {
            this.repository = repository;
        }

        public List<Item> FindByCategory(Category category)
        {
            return repository.FindAll()
                    .Where(i => i.Category == category)
                    .ToList();
        }

        public Result<Item> Add(Item item)
        {
            var result = new Result<Item>();
            if(item == null)
            {
                result.AddMessage("Item must not be null.");
                return result;
            }

            if(string.IsNullOrWhiteSpace(item.Name))
            {
                result.AddMessage("Item name is required.");
            }
            else if(repository.FindAll()
                  .Any(i => i.Name.Equals(item.Name, StringComparison.OrdinalIgnoreCase)))
            {
                result.AddMessage($"Item '{item.Name}' is a duplicate.");
            }

            if(item.DollarsPerKilogram < 0M || item.DollarsPerKilogram > 7500M)
            {
                result.AddMessage("$/Kg must be between 0.00 and 7500.00.");
            }

            if(!result.Success)
            {
                return result;
            }

            result.Value = repository.Add(item);

            return result;
        }

        public Dictionary<Category, decimal> TotalValueOfCategory(List<Forage> forages)
        {
            /*
            Dictionary<Category, decimal> totalValueOfCat = new();
            foreach (var forage in forages)
            {
                if (totalValueOfCat.ContainsKey(forage.Item.Category))
                {
                    totalValueOfCat[forage.Item.Category] += forage.Value;
                }
                else
                {
                    totalValueOfCat[forage.Item.Category] = forage.Value;
                }
            }
            return totalValueOfCat;
            */
            
            Dictionary<Category, decimal> totalValueOfCat = forages.GroupBy(f => f.Item.Category).ToDictionary(f => f.Key, f => f.Sum(f => f.Value));
            return totalValueOfCat;
            
        }



    }
}
