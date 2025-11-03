using CityInfo.API;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CityInfo.API.Tests;

[TestClass]
public class CitiesDataStoreTests
{
    [TestMethod]
    public void Current_Returns_Instance_With_Cities()
    {
        // Act
        var dataStore = CitiesDataStore.Current;

        // Assert
        Assert.IsNotNull(dataStore);
        Assert.IsTrue(dataStore.Cities.Count > 0);
    }
}
