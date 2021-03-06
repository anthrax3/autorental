﻿using AutoRental.Business.Common;
using AutoRental.Business.Entities;
using AutoRental.Common;
using AutoRental.Data.Contracts;
using Core.Common.Contracts;
using Core.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;

namespace AutoRental.Business
{
   [Export(typeof(IAutoRentalEngine))]  // MefDI: interface mapping
   [PartCreationPolicy(CreationPolicy.NonShared)] // MEfDI: non-singleton
   public class AutoRentalEngine : IAutoRentalEngine
   {
      #region Properties

      IDataRepositoryFactory _DataRepositoryFactory;
      #endregion
      //-----------------------------------------------------------------------------------------------------

      #region Constructors

      [ImportingConstructor]
      public AutoRentalEngine(IDataRepositoryFactory dataRepositoryFactory)
      {
         _DataRepositoryFactory = dataRepositoryFactory;
      }
      #endregion
      //-----------------------------------------------------------------------------------------------------

      #region Methods

      public bool IsCarCurrentlyRented(int carId, int accountId)
      {
         bool rented = false;

         IRentalRepository rentalRepository = _DataRepositoryFactory.GetDataRepository<IRentalRepository>();

         var currentRental = rentalRepository.GetCurrentRentalByCar(carId);
         if (currentRental != null && currentRental.AccountId == accountId)
            rented = true;

         return rented;
      }

      public bool IsCarCurrentlyRented(int carId)
      {
         bool rented = false;

         IRentalRepository rentalRepository = _DataRepositoryFactory.GetDataRepository<IRentalRepository>();

         Rental currentRental = rentalRepository.GetCurrentRentalByCar(carId);
         if (currentRental != null)
            rented = true;

         return rented;
      }

      public bool IsCarAvailableForRental(int carId, DateTime pickupDate, DateTime returnDate, IEnumerable<Rental> rentedCars, IEnumerable<Reservation> reservedCars)
      {
         bool available = true;

         Reservation reservation = reservedCars.Where(item => item.CarId == carId).FirstOrDefault();
         if (reservation != null && (
            (pickupDate >= reservation.RentalDate && pickupDate <= reservation.ReturnDate) ||
            (returnDate >= reservation.RentalDate && returnDate <= reservation.ReturnDate)))
         {
            available = false;
         }

         if (available)
         {
            Rental rental = rentedCars.Where(item => item.CarId == carId).FirstOrDefault();
            if (rental != null && (pickupDate <= rental.DateDue))
               available = false;
         }

         return available;
      }

      public Rental RentCarToCustomer(string loginEmail, int carId, DateTime rentalDate, DateTime dateDueBack)
      {
         if (rentalDate > DateTime.Now) // post-dated rentals are not allowed 
            throw new UnableToRentForDateException(string.Format("Cannot rent for date {0} yet.", rentalDate.ToShortDateString()));

         IAccountRepository accountRepository = _DataRepositoryFactory.GetDataRepository<IAccountRepository>();
         IRentalRepository rentalRepository = _DataRepositoryFactory.GetDataRepository<IRentalRepository>();

         bool carIsRented = IsCarCurrentlyRented(carId); // make sure car is not already rented
         if (carIsRented)
            throw new CarCurrentlyRentedException(string.Format("Car {0} is already rented.", carId));

         Account account = accountRepository.GetByLogin(loginEmail); // make sure account exists
         if (account == null)
            throw new NotFoundException(string.Format("No account found for login '{0}'.", loginEmail));

         Rental rental = new Rental() // create the rental for the account
         {
            AccountId = account.AccountId,
            CarId = carId,
            DateRented = rentalDate,
            DateDue = dateDueBack
         };

         Rental savedEntity = rentalRepository.Add(rental); // save the rental

         return savedEntity;
      }
      #endregion
   }
}
