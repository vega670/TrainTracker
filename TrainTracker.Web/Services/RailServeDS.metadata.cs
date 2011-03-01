
namespace TrainTracker.Web.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Data.Objects.DataClasses;
    using System.Linq;
    using System.ServiceModel.DomainServices.Hosting;
    using System.ServiceModel.DomainServices.Server;


    // The MetadataTypeAttribute identifies ActivityMetadata as the class
    // that carries additional metadata for the Activity class.
    [MetadataTypeAttribute(typeof(Activity.ActivityMetadata))]
    public partial class Activity
    {

        // This class allows you to attach custom attributes to properties
        // of the Activity class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class ActivityMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private ActivityMetadata()
            {
            }

            public string ActivityCode { get; set; }

            public int ActivityId { get; set; }

            public string ActivityName { get; set; }

            public EntityCollection<RailCarCurrentStatu> RailCarCurrentStatus { get; set; }

            public EntityCollection<RailCarHistory> RailCarHistories { get; set; }

            public EntityCollection<RailYard_Activities> RailYard_Activities { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies CarLoadStatuMetadata as the class
    // that carries additional metadata for the CarLoadStatu class.
    [MetadataTypeAttribute(typeof(CarLoadStatu.CarLoadStatuMetadata))]
    public partial class CarLoadStatu
    {

        // This class allows you to attach custom attributes to properties
        // of the CarLoadStatu class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class CarLoadStatuMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private CarLoadStatuMetadata()
            {
            }

            public EntityCollection<RailCarCurrentStatu> RailCarCurrentStatus { get; set; }

            public EntityCollection<RailCarHistory> RailCarHistories { get; set; }

            public string StatusCode { get; set; }

            public int StatusID { get; set; }

            public string StatusName { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies CommodityMetadata as the class
    // that carries additional metadata for the Commodity class.
    [MetadataTypeAttribute(typeof(Commodity.CommodityMetadata))]
    public partial class Commodity
    {

        // This class allows you to attach custom attributes to properties
        // of the Commodity class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class CommodityMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private CommodityMetadata()
            {
            }

            public string Comments { get; set; }

            public string CommodityCode { get; set; }

            public int CommodityID { get; set; }

            public string CommodityName { get; set; }

            public EntityCollection<RailCarCurrentStatu> RailCarCurrentStatus { get; set; }

            public EntityCollection<RailCarHistory> RailCarHistories { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies HistoryTypeMetadata as the class
    // that carries additional metadata for the HistoryType class.
    [MetadataTypeAttribute(typeof(HistoryType.HistoryTypeMetadata))]
    public partial class HistoryType
    {

        // This class allows you to attach custom attributes to properties
        // of the HistoryType class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class HistoryTypeMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private HistoryTypeMetadata()
            {
            }

            public string HistoryType1 { get; set; }

            public int HistoryTypeID { get; set; }

            public EntityCollection<RailCarCurrentStatu> RailCarCurrentStatus { get; set; }

            public EntityCollection<RailCarHistory> RailCarHistories { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies RailCarMetadata as the class
    // that carries additional metadata for the RailCar class.
    [MetadataTypeAttribute(typeof(RailCar.RailCarMetadata))]
    public partial class RailCar
    {

        // This class allows you to attach custom attributes to properties
        // of the RailCar class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class RailCarMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private RailCarMetadata()
            {
            }

            public int CarID { get; set; }

            public string Number { get; set; }

            public string Owner { get; set; }

            public EntityCollection<RailCarCurrentStatu> RailCarCurrentStatus { get; set; }

            public EntityCollection<RailCarHistory> RailCarHistories { get; set; }
             [Include]
            public RailCarType RailCarType { get; set; }

            public Nullable<int> Type { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies RailCarCurrentStatuMetadata as the class
    // that carries additional metadata for the RailCarCurrentStatu class.
    [MetadataTypeAttribute(typeof(RailCarCurrentStatu.RailCarCurrentStatuMetadata))]
    public partial class RailCarCurrentStatu
    {

        // This class allows you to attach custom attributes to properties
        // of the RailCarCurrentStatu class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class RailCarCurrentStatuMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private RailCarCurrentStatuMetadata()
            {
            }
            [Include]
            public Activity Activity { get; set; }

            public Nullable<int> ActivityId { get; set; }

            public int CarID { get; set; }
            [Include]
            public CarLoadStatu CarLoadStatu { get; set; }

            public string Comments { get; set; }
            [Include]
            public Commodity Commodity { get; set; }

            public Nullable<int> CommodityId { get; set; }

            public string Company { get; set; }

            public int CurrentStatusID { get; set; }
            [Include]
            public HistoryType HistoryType { get; set; }

            public int HistoryTypeId { get; set; }

            public string PrimaryUser { get; set; }
            [Include]
            public RailCar RailCar { get; set; }
            [Include]
            public RailYard RailYard { get; set; }

            public Nullable<DateTime> ReceiptDate { get; set; }

            public Nullable<DateTime> ReceiptTime { get; set; }

            public string SecondaryUser { get; set; }

            public Nullable<DateTime> ShipDate { get; set; }

            public Nullable<DateTime> ShipTime { get; set; }

            public Nullable<int> Spot { get; set; }

            public Nullable<int> StatusId { get; set; }

            public string Supplier { get; set; }
            [Include]
            public Track Track { get; set; }

            public Nullable<int> TrackId { get; set; }

            public Nullable<int> Weight { get; set; }

            public int YardID { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies RailCarHistoryMetadata as the class
    // that carries additional metadata for the RailCarHistory class.
    [MetadataTypeAttribute(typeof(RailCarHistory.RailCarHistoryMetadata))]
    public partial class RailCarHistory
    {

        // This class allows you to attach custom attributes to properties
        // of the RailCarHistory class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class RailCarHistoryMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private RailCarHistoryMetadata()
            {
            }

            public Activity Activity { get; set; }

            public Nullable<int> ActivityId { get; set; }

            public int CarID { get; set; }

            public CarLoadStatu CarLoadStatu { get; set; }

            public string Comments { get; set; }

            public Commodity Commodity { get; set; }

            public Nullable<int> CommodityId { get; set; }

            public string Company { get; set; }

            public int HistoryID { get; set; }

            public HistoryType HistoryType { get; set; }

            public int HistoryTypeId { get; set; }

            public string PrimaryUser { get; set; }

            public RailCar RailCar { get; set; }

            public RailYard RailYard { get; set; }

            public Nullable<DateTime> ReceiptDate { get; set; }

            public Nullable<DateTime> ReceiptTime { get; set; }

            public string SecondaryUser { get; set; }

            public Nullable<DateTime> ShipDate { get; set; }

            public Nullable<DateTime> ShipTime { get; set; }

            public Nullable<int> Spot { get; set; }

            public Nullable<int> StatusId { get; set; }

            public string Supplier { get; set; }

            public Track Track { get; set; }

            public Nullable<int> TrackId { get; set; }

            public Nullable<int> Weight { get; set; }

            public int YardID { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies RailCarTypeMetadata as the class
    // that carries additional metadata for the RailCarType class.
    [MetadataTypeAttribute(typeof(RailCarType.RailCarTypeMetadata))]
    public partial class RailCarType
    {

        // This class allows you to attach custom attributes to properties
        // of the RailCarType class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class RailCarTypeMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private RailCarTypeMetadata()
            {
            }

            public Nullable<int> Length { get; set; }
             
            public EntityCollection<RailCar> RailCars { get; set; }
           
            public string Type { get; set; }

            public int TypeID { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies RailYardMetadata as the class
    // that carries additional metadata for the RailYard class.
    [MetadataTypeAttribute(typeof(RailYard.RailYardMetadata))]
    public partial class RailYard
    {

        // This class allows you to attach custom attributes to properties
        // of the RailYard class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class RailYardMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private RailYardMetadata()
            {
            }

            public string City { get; set; }

            public string County { get; set; }

            public string Phone { get; set; }

           
            public EntityCollection<RailCarCurrentStatu> RailCarCurrentStatus { get; set; }

            public EntityCollection<RailCarHistory> RailCarHistories { get; set; }

            public EntityCollection<RailYard_Activities> RailYard_Activities { get; set; }

            public string State { get; set; }

            public string Street { get; set; }
             
            public EntityCollection<Track> Tracks { get; set; }

            public string YardCode { get; set; }

            public int YardID { get; set; }

            public string YardName { get; set; }

            public Nullable<int> Zip { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies RailYard_ActivitiesMetadata as the class
    // that carries additional metadata for the RailYard_Activities class.
    [MetadataTypeAttribute(typeof(RailYard_Activities.RailYard_ActivitiesMetadata))]
    public partial class RailYard_Activities
    {

        // This class allows you to attach custom attributes to properties
        // of the RailYard_Activities class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class RailYard_ActivitiesMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private RailYard_ActivitiesMetadata()
            {
            }

            public Activity Activity { get; set; }

            public Nullable<int> ActivityID { get; set; }

            public int ID { get; set; }

            public RailYard RailYard { get; set; }

            public Nullable<int> YardID { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies TrackMetadata as the class
    // that carries additional metadata for the Track class.
    [MetadataTypeAttribute(typeof(Track.TrackMetadata))]
    public partial class Track
    {

        // This class allows you to attach custom attributes to properties
        // of the Track class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class TrackMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private TrackMetadata()
            {
            }

            public Nullable<int> Length { get; set; }

            public Nullable<int> MaxCars { get; set; }

            public EntityCollection<RailCarCurrentStatu> RailCarCurrentStatus { get; set; }

            public EntityCollection<RailCarHistory> RailCarHistories { get; set; }
            [Include]
            public RailYard RailYard { get; set; }

            public string Track1 { get; set; }

            public int TrackID { get; set; }
            
            public Nullable<int> YardId { get; set; }
        }
    }
}
