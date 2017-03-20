using NP.Core.Common.Core;
using System;

namespace AutoRental.Client.Entities
{
   public class Reservation : ObjectBase
   {
      #region Properties

      int _ReservationId;
      int _AccountId;
      int _CarId;
      DateTime _ReturnDate;
      DateTime _RentalDate;
      #endregion
      //-----------------------------------------------------------------------------------------------------

      #region Accessors

      public int ReservationId
      {
         get { return _ReservationId; }
         set
         {
            if (_ReservationId != value)
            {
               _ReservationId = value;
               OnPropertyChanged(() => ReservationId);
            }
         }
      }

      public int AccountId
      {
         get { return _AccountId; }
         set
         {
            if (_AccountId != value)
            {
               _AccountId = value;
               OnPropertyChanged(() => AccountId);
            }
         }
      }

      public int CarId
      {
         get { return _CarId; }
         set
         {
            if (_CarId != value)
            {
               _CarId = value;
               OnPropertyChanged(() => CarId);
            }
         }
      }

      public DateTime RentalDate
      {
         get { return _RentalDate; }
         set
         {
            if (_RentalDate != value)
            {
               _RentalDate = value;
               OnPropertyChanged(() => RentalDate);
            }
         }
      }

      public DateTime ReturnDate
      {
         get { return _ReturnDate; }
         set
         {
            if (_ReturnDate != value)
            {
               _ReturnDate = value;
               OnPropertyChanged(() => ReturnDate);
            }
         }
      }
      #endregion
   }
}
