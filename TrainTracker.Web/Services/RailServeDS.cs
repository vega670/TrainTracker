
namespace TrainTracker.Web.Services
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Data;
    using System.Linq;
    using System.ServiceModel.DomainServices.EntityFramework;
    using System.ServiceModel.DomainServices.Hosting;
    using System.ServiceModel.DomainServices.Server;
    using TrainTracker.Web.Models;


    // Implements application logic using the RailServe_dataEntities1 context.
    // TODO: Add your application logic to these methods or in additional methods.
    // TODO: Wire up authentication (Windows/ASP.NET Forms) and uncomment the following to disable anonymous access
    // Also consider adding roles to restrict access as appropriate.
    // [RequiresAuthentication]
    [EnableClientAccess()]
    public class RailServeDS : LinqToEntitiesDomainService<RailServe_dataEntities1>
    {

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Activities' query.
        public IQueryable<Activity> GetActivities()
        {
            return this.ObjectContext.Activities;
        }

        public void InsertActivity(Activity activity)
        {
            if ((activity.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(activity, EntityState.Added);
            }
            else
            {
                this.ObjectContext.Activities.AddObject(activity);
            }
        }

        public void UpdateActivity(Activity currentActivity)
        {
            this.ObjectContext.Activities.AttachAsModified(currentActivity, this.ChangeSet.GetOriginal(currentActivity));
        }

        public void DeleteActivity(Activity activity)
        {
            if ((activity.EntityState == EntityState.Detached))
            {
                this.ObjectContext.Activities.Attach(activity);
            }
            this.ObjectContext.Activities.DeleteObject(activity);
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'CarLoadStatus' query.
        public IQueryable<CarLoadStatu> GetCarLoadStatus()
        {
            return this.ObjectContext.CarLoadStatus;
        }

        public void InsertCarLoadStatu(CarLoadStatu carLoadStatu)
        {
            if ((carLoadStatu.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(carLoadStatu, EntityState.Added);
            }
            else
            {
                this.ObjectContext.CarLoadStatus.AddObject(carLoadStatu);
            }
        }

        public void UpdateCarLoadStatu(CarLoadStatu currentCarLoadStatu)
        {
            this.ObjectContext.CarLoadStatus.AttachAsModified(currentCarLoadStatu, this.ChangeSet.GetOriginal(currentCarLoadStatu));
        }

        public void DeleteCarLoadStatu(CarLoadStatu carLoadStatu)
        {
            if ((carLoadStatu.EntityState == EntityState.Detached))
            {
                this.ObjectContext.CarLoadStatus.Attach(carLoadStatu);
            }
            this.ObjectContext.CarLoadStatus.DeleteObject(carLoadStatu);
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Commodities' query.
        public IQueryable<Commodity> GetCommodities()
        {
            return this.ObjectContext.Commodities;
        }

        public void InsertCommodity(Commodity commodity)
        {
            if ((commodity.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(commodity, EntityState.Added);
            }
            else
            {
                this.ObjectContext.Commodities.AddObject(commodity);
            }
        }

        public void UpdateCommodity(Commodity currentCommodity)
        {
            this.ObjectContext.Commodities.AttachAsModified(currentCommodity, this.ChangeSet.GetOriginal(currentCommodity));
        }

        public void DeleteCommodity(Commodity commodity)
        {
            if ((commodity.EntityState == EntityState.Detached))
            {
                this.ObjectContext.Commodities.Attach(commodity);
            }
            this.ObjectContext.Commodities.DeleteObject(commodity);
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'HistoryTypes' query.
        public IQueryable<HistoryType> GetHistoryTypes()
        {
            return this.ObjectContext.HistoryTypes;
        }

        public void InsertHistoryType(HistoryType historyType)
        {
            if ((historyType.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(historyType, EntityState.Added);
            }
            else
            {
                this.ObjectContext.HistoryTypes.AddObject(historyType);
            }
        }

        public void UpdateHistoryType(HistoryType currentHistoryType)
        {
            this.ObjectContext.HistoryTypes.AttachAsModified(currentHistoryType, this.ChangeSet.GetOriginal(currentHistoryType));
        }

        public void DeleteHistoryType(HistoryType historyType)
        {
            if ((historyType.EntityState == EntityState.Detached))
            {
                this.ObjectContext.HistoryTypes.Attach(historyType);
            }
            this.ObjectContext.HistoryTypes.DeleteObject(historyType);
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'RailCars' query.
        public IQueryable<RailCar> GetRailCars()
        {
            return this.ObjectContext.RailCars.Include("RailCarType");
        }

        public void InsertRailCar(RailCar railCar)
        {
            if ((railCar.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(railCar, EntityState.Added);
            }
            else
            {
                this.ObjectContext.RailCars.AddObject(railCar);
            }
        }

        public void UpdateRailCar(RailCar currentRailCar)
        {
            this.ObjectContext.RailCars.AttachAsModified(currentRailCar, this.ChangeSet.GetOriginal(currentRailCar));
        }

        public void DeleteRailCar(RailCar railCar)
        {
            if ((railCar.EntityState == EntityState.Detached))
            {
                this.ObjectContext.RailCars.Attach(railCar);
            }
            this.ObjectContext.RailCars.DeleteObject(railCar);
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'RailCarCurrentStatus' query.
        public IQueryable<RailCarCurrentStatu> GetRailCarCurrentStatus()
        {
            return this.ObjectContext.RailCarCurrentStatus;
        }
        public IQueryable<RailCarCurrentStatu> GetRailCarCurrentStatusById(int yard)
        {
            return this.ObjectContext.RailCarCurrentStatus.Include("CarLoadStatu").Include("Commodity").Include("HistoryType").Include("RailCar").Include("RailYard").Include("Track").Where(y => y.YardID == yard);
        }


        public void InsertRailCarCurrentStatu(RailCarCurrentStatu railCarCurrentStatu)
        {
            if ((railCarCurrentStatu.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(railCarCurrentStatu, EntityState.Added);
            }
            else
            {
                this.ObjectContext.RailCarCurrentStatus.AddObject(railCarCurrentStatu);
            }
        }

        public void UpdateRailCarCurrentStatu(RailCarCurrentStatu currentRailCarCurrentStatu)
        {
            this.ObjectContext.RailCarCurrentStatus.AttachAsModified(currentRailCarCurrentStatu, this.ChangeSet.GetOriginal(currentRailCarCurrentStatu));
        }

        public void DeleteRailCarCurrentStatu(RailCarCurrentStatu railCarCurrentStatu)
        {
            if ((railCarCurrentStatu.EntityState == EntityState.Detached))
            {
                this.ObjectContext.RailCarCurrentStatus.Attach(railCarCurrentStatu);
            }
            this.ObjectContext.RailCarCurrentStatus.DeleteObject(railCarCurrentStatu);
        }
        public void currentstatus_set(RailCarCurrentStatu railCarCurrentStatu)
        {
            if ((railCarCurrentStatu.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(railCarCurrentStatu, EntityState.Added);
            }
            else
            {
                this.ObjectContext.sp_car_currentstatus_set(railCarCurrentStatu.CarID, railCarCurrentStatu.YardID, railCarCurrentStatu.HistoryTypeId, railCarCurrentStatu.ActivityId, railCarCurrentStatu.TrackId, railCarCurrentStatu.Spot, railCarCurrentStatu.StatusId, railCarCurrentStatu.CommodityId, railCarCurrentStatu.Comments, railCarCurrentStatu.Company, railCarCurrentStatu.Supplier, railCarCurrentStatu.Weight, railCarCurrentStatu.ReceiptDate, railCarCurrentStatu.ReceiptTime, railCarCurrentStatu.ShipDate, railCarCurrentStatu.ShipTime, railCarCurrentStatu.PrimaryUser, railCarCurrentStatu.SecondaryUser);            
            }
             }        

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'RailCarHistories' query.
        public IQueryable<RailCarHistory> GetRailCarHistories()
        {
            return this.ObjectContext.RailCarHistories;
        }

        public void InsertRailCarHistory(RailCarHistory railCarHistory)
        {
            if ((railCarHistory.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(railCarHistory, EntityState.Added);
            }
            else
            {
                this.ObjectContext.RailCarHistories.AddObject(railCarHistory);
            }
        }

        public void UpdateRailCarHistory(RailCarHistory currentRailCarHistory)
        {
            this.ObjectContext.RailCarHistories.AttachAsModified(currentRailCarHistory, this.ChangeSet.GetOriginal(currentRailCarHistory));
        }

        public void DeleteRailCarHistory(RailCarHistory railCarHistory)
        {
            if ((railCarHistory.EntityState == EntityState.Detached))
            {
                this.ObjectContext.RailCarHistories.Attach(railCarHistory);
            }
            this.ObjectContext.RailCarHistories.DeleteObject(railCarHistory);
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'RailCarTypes' query.
        public IQueryable<RailCarType> GetRailCarTypes()
        {
            return this.ObjectContext.RailCarTypes;
        }

        public void InsertRailCarType(RailCarType railCarType)
        {
            if ((railCarType.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(railCarType, EntityState.Added);
            }
            else
            {
                this.ObjectContext.RailCarTypes.AddObject(railCarType);
            }
        }

        public void UpdateRailCarType(RailCarType currentRailCarType)
        {
            this.ObjectContext.RailCarTypes.AttachAsModified(currentRailCarType, this.ChangeSet.GetOriginal(currentRailCarType));
        }

        public void DeleteRailCarType(RailCarType railCarType)
        {
            if ((railCarType.EntityState == EntityState.Detached))
            {
                this.ObjectContext.RailCarTypes.Attach(railCarType);
            }
            this.ObjectContext.RailCarTypes.DeleteObject(railCarType);
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'RailYards' query.
        public IQueryable<RailYard> GetRailYards()
        {
            return this.ObjectContext.RailYards;
        }

        public void InsertRailYard(RailYard railYard)
        {
            if ((railYard.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(railYard, EntityState.Added);
            }
            else
            {
                this.ObjectContext.RailYards.AddObject(railYard);
            }
        }

        public void UpdateRailYard(RailYard currentRailYard)
        {
            this.ObjectContext.RailYards.AttachAsModified(currentRailYard, this.ChangeSet.GetOriginal(currentRailYard));
        }

        public void DeleteRailYard(RailYard railYard)
        {
            if ((railYard.EntityState == EntityState.Detached))
            {
                this.ObjectContext.RailYards.Attach(railYard);
            }
            this.ObjectContext.RailYards.DeleteObject(railYard);
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'RailYard_Activities' query.
        public IQueryable<RailYard_Activities> GetRailYard_Activities()
        {
            return this.ObjectContext.RailYard_Activities;
        }

        public void InsertRailYard_Activities(RailYard_Activities railYard_Activities)
        {
            if ((railYard_Activities.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(railYard_Activities, EntityState.Added);
            }
            else
            {
                this.ObjectContext.RailYard_Activities.AddObject(railYard_Activities);
            }
        }

        public void UpdateRailYard_Activities(RailYard_Activities currentRailYard_Activities)
        {
            this.ObjectContext.RailYard_Activities.AttachAsModified(currentRailYard_Activities, this.ChangeSet.GetOriginal(currentRailYard_Activities));
        }

        public void DeleteRailYard_Activities(RailYard_Activities railYard_Activities)
        {
            if ((railYard_Activities.EntityState == EntityState.Detached))
            {
                this.ObjectContext.RailYard_Activities.Attach(railYard_Activities);
            }
            this.ObjectContext.RailYard_Activities.DeleteObject(railYard_Activities);
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Tracks' query.
        public IQueryable<Track> GetTracks()
        {
            return this.ObjectContext.Tracks;
        }
        public IQueryable<Track> GetTracksByYardID(int yard)
        {
            return this.ObjectContext.Tracks.Where(y => y.YardId == yard);
        }

        public void InsertTrack(Track track)
        {
            if ((track.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(track, EntityState.Added);
            }
            else
            {
                this.ObjectContext.Tracks.AddObject(track);
            }
        }

        public void UpdateTrack(Track currentTrack)
        {
            this.ObjectContext.Tracks.AttachAsModified(currentTrack, this.ChangeSet.GetOriginal(currentTrack));
        }

        public void DeleteTrack(Track track)
        {
            if ((track.EntityState == EntityState.Detached))
            {
                this.ObjectContext.Tracks.Attach(track);
            }
            this.ObjectContext.Tracks.DeleteObject(track);
        }
    }
}


