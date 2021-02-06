﻿using System;
using NUnit.Framework;
using ParkingService.Domain.Entities;

namespace ParkingService.Domain.Tests
{
    public class ParkingTests
    {
        [Test]
        public void AddParkingLevel()
        {
            var parking = new Parking(Address.BelarusMinskNemiga);
            var parkingLevel = new Floor(1);

            var result = parking.AddFloor(parkingLevel);

            Assert.IsTrue(result.IsSuccess);
        }

        [Test]
        public void AddExistsParkingLevel()
        {
            var parking = new Parking(Address.BelarusMinskNemiga);
            var parkingLevel1 = new Floor(1);
            var parkingLevel2 = new Floor(1);

            var result1 = parking.AddFloor(parkingLevel1);
            var result2 = parking.AddFloor(parkingLevel2);

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