using System.Collections.Generic;
using MakeNotes.Common.Models;
using MakeNotes.Notebook.Collections;
using MakeNotes.Notebook.Consts;
using Xunit;

namespace MakeNotes.UnitTests.Modules.Notebook
{
    public class NavbarTabItemObservableCollectionTests
    {
        private static void AssertDefaultItem(NavbarTabItemObservableCollection collection)
        {
            var item = collection[0];
            Assert.Null(item.Id);
            Assert.Equal(DefaultValues.DefaultTabName, item.Header);
            Assert.Equal(0, item.Order);
        }

        private static void AssertTwoItems(NavbarTabItem expectedItem, NavbarTabItem actualItem)
        {
            Assert.Equal(expectedItem.Id, actualItem.Id);
            Assert.Equal(expectedItem.Header, actualItem.Header);
            Assert.Equal(expectedItem.Order, actualItem.Order);
        }

        [Fact]
        public void ShouldCreateDefaultItem_WhenInitializedWithEmptyConstructor()
        {
            var collection = new NavbarTabItemObservableCollection();

            Assert.Single(collection);
            AssertDefaultItem(collection);
        }

        [Fact]
        public void ShouldCreateDefaultItem_WhenInitializedWithEmptyInputCollection()
        {
            var emptyCollection = new List<NavbarTabItem>();

            var collection = new NavbarTabItemObservableCollection(emptyCollection);

            Assert.Single(collection);
            AssertDefaultItem(collection);
        }

        [Fact]
        public void ShouldCreateDefaultItem_WhenAllItemsAreRemoved()
        {
            var collection = new NavbarTabItemObservableCollection();
            collection.Add(new NavbarTabItem("test1", 1));
            collection.Add(new NavbarTabItem("test2", 2));

            collection.Clear();

            Assert.Single(collection);
            AssertDefaultItem(collection);
        }

        [Fact]
        public void ShouldCreateDefaultItem_WhenAllItemsAreRemovedOneByOne()
        {
            var collection = new NavbarTabItemObservableCollection();
            collection.Add(new NavbarTabItem("test1", 1));
            collection.Add(new NavbarTabItem("test2", 2));

            var item1 = collection[0];
            var item2 = collection[1];
            var item3 = collection[2];

            collection.Remove(item1);
            collection.Remove(item2);
            collection.Remove(item3);

            Assert.Single(collection);
            AssertDefaultItem(collection);
        }

        [Fact]
        public void ShouldBeInitializedWithItems_WhenInputCollectionIsGiven()
        {
            var inputCollection = new List<NavbarTabItem>
            {
                new NavbarTabItem("test1", 0),
                new NavbarTabItem("test2", 1)
            };

            var collection = new NavbarTabItemObservableCollection(inputCollection);

            Assert.Equal(2, collection.Count);
            AssertTwoItems(inputCollection[0], collection[0]);
            AssertTwoItems(inputCollection[1], collection[1]);
        }
    }
}
