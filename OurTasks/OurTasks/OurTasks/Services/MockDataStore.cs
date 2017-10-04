using OurTasks.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OurTasks.Services
{
    public class MockDataStore: IDataStore<Item>
    {
        List<Item> items;

        public MockDataStore()
        {
            items = new List<Item>();
            var mockItems = new List<Item>
            {
                new Item
                {
                    Id = Guid.NewGuid().ToString(),
                    Text = "Clean litter box",
                    Description ="Monthly cleaning of litter box",
                    Location="Home",
                    AssignedTo="Robert",
                    CreatedDate=DateTime.Today,
                    DueDate=DateTime.Today.AddDays(2)
                },
                new Item
                {
                    Id = Guid.NewGuid().ToString(),
                    Text = "Koko care",
                    Description ="Monthly nail clipping and flea protection for the cat",
                    Location="Home",
                    AssignedTo="Colette",
                    CreatedDate=DateTime.Today,
                    DueDate=DateTime.Today.AddDays(2)
                },
                new Item
                {
                    Id = Guid.NewGuid().ToString(),
                    Text = "Book vacation travel",
                    Description ="2 round trip tickets to somewhere fun",
                    Location="Home",
                    AssignedTo="Robert",
                    CreatedDate=DateTime.Today,
                    DueDate=DateTime.Today.AddDays(-2)
                },
                new Item
                {
                    Id = Guid.NewGuid().ToString(),
                    Text = "Mow the lawn",
                    Description ="Might have to buy gas first",
                    Location="Home",
                    AssignedTo="Robert",
                    CreatedDate=DateTime.Today,
                    DueDate=DateTime.Today.AddDays(5)
                },
            };

            foreach (var item in mockItems)
            {
                items.Add(item);
            }
        }

        public async Task<bool> AddItemAsync(Item item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Item item)
        {
            var _item = items.Where((Item arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(_item);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var _item = items.Where((Item arg) => arg.Id == id).FirstOrDefault();
            items.Remove(_item);

            return await Task.FromResult(true);
        }

        public async Task<Item> GetItemAsync(string id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Item>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }
    }

}

