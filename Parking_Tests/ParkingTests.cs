using NUnit.Framework;
using Parking_Domain;
using System;
using Parking_Domain.Entities;

namespace Parking_Tests
{
    public class ParkingTests
    {
        [Test]
        public void AddParkingLevel()
        {
            var parking = new Parking(Address.BelarusMinskNemiga);
            var parkingLevel = new ParkingLevel(1);

            var result = parking.AddParkingLevel(parkingLevel);

            Assert.IsTrue(result.IsSuccess);
        }

        [Test]
        public void AddExistsParkingLevel()
        {
            var parking = new Parking(Address.BelarusMinskNemiga);
            var parkingLevel1 = new ParkingLevel(1);
            var parkingLevel2 = new ParkingLevel(1);

            var result1 = parking.AddParkingLevel(parkingLevel1);
            var result2 = parking.AddParkingLevel(parkingLevel2);

            Assert.IsTrue(result1.IsSuccess);
            Assert.IsFalse(result2.IsSuccess);
        }

        [Test]
        public void SetCorrectAddress()
        {
            var parking = new Parking(Address.BelarusMinskNemiga);

            parking.UpdateAddress(Address.BelarusMinskVostok);

            Assert.AreEqual(parking.Address, Address.BelarusMinskVostok);
        }

        [Test]
        public void SetIncorrectAddress()
        {
            var parking = new Parking(Address.BelarusMinskNemiga);

            void TestDelegate() => parking.UpdateAddress(null);

            Assert.Throws(typeof(ArgumentNullException), TestDelegate);
        }
    }
}