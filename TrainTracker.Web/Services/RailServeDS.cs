
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


    // Implements application logic using the RailServe_dataEntities3 context.
    // TODO: Add your application logic to these methods or in additional methods.
    // TODO: Wire up authentication (Windows/ASP.NET Forms) and uncomment the following to disable anonymous access
    // Also consider adding roles to restrict access as appropriate.
    // [RequiresAuthentication]
    [EnableClientAccess()]
    public class RailServeDS : LinqToEntitiesDomainService<RailServe_dataEntities3>
    {

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
        public IQueryable<Commodity> GetCommoditiesByLocation(int location)
        {
            return this.ObjectContext.Commodities.Where(y => y.LocationID == location);
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
        // To support paging you will need to add ordering to the 'Departments' query.
        public IQueryable<Department> GetDepartments()
        {
            return this.ObjectContext.Departments;
        }
        public IQueryable<Department> GetDepartmentsByLocation(int location)
        {
            return this.ObjectContext.Departments.Where(y => y.LocationID == location);
        }

        public void InsertDepartment(Department department)
        {
            if ((department.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(department, EntityState.Added);
            }
            else
            {
                this.ObjectContext.Departments.AddObject(department);
            }
        }

        public void UpdateDepartment(Department currentDepartment)
        {
            this.ObjectContext.Departments.AttachAsModified(currentDepartment, this.ChangeSet.GetOriginal(currentDepartment));
        }

        public void DeleteDepartment(Department department)
        {
            if ((department.EntityState == EntityState.Detached))
            {
                this.ObjectContext.Departments.Attach(department);
            }
            this.ObjectContext.Departments.DeleteObject(department);
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
        // To support paging you will need to add ordering to the 'Locations' query.
        public IQueryable<Location> GetLocations()
        {
            return this.ObjectContext.Locations;
        }

        public void InsertLocation(Location location)
        {
            if ((location.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(location, EntityState.Added);
            }
            else
            {
                this.ObjectContext.Locations.AddObject(location);
            }
        }

        public void UpdateLocation(Location currentLocation)
        {
            this.ObjectContext.Locations.AttachAsModified(currentLocation, this.ChangeSet.GetOriginal(currentLocation));
        }

        public void DeleteLocation(Location location)
        {
            if ((location.EntityState == EntityState.Detached))
            {
                this.ObjectContext.Locations.Attach(location);
            }
            this.ObjectContext.Locations.DeleteObject(location);
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'RailCars' query.
        public IQueryable<RailCar> GetRailCars()
        {
            return this.ObjectContext.RailCars.Include("RailCarType");
        }

        public RailCar GetRailCarByNumber(string number)
        {
            return (RailCar) this.ObjectContext.RailCars.Where(y => y.Number == number);
        }
        public int GetRailCarsByNumber(string number)
        {
            return this.ObjectContext.RailCars.Where(y => y.Number == number).Count();
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
        public IQueryable<RailCarCurrentStatu> GetRailCarCurrentStatusByLocation(int location)
        {
            return this.ObjectContext.RailCarCurrentStatus.Include("CarLoadStatu").Include("Commodity").Include("Location").Include("Department").Include("HistoryType").Include("RailCar").Include("RailYard").Include("Track").Include("RailCar.RailCarType").Where(y => y.LocationID == location);
        }
        public IQueryable<RailCarCurrentStatu> GetRailCarCurrentStatusByTrack(int track)
        {
            return this.ObjectContext.RailCarCurrentStatus.Include("CarLoadStatu").Include("Commodity").Include("Location").Include("Department").Include("HistoryType").Include("RailCar").Include("RailYard").Include("Track").Include("RailCar.RailCarType").Where(y => y.TrackId == track);
        }
        public IQueryable<RailCarCurrentStatu> GetRailCarCurrentStatusByLocationandTrack(int location, int track)
        {
            return this.ObjectContext.RailCarCurrentStatus.Where(y => y.LocationID == location && y.TrackId == track);
        }
        [Invoke]
        public int GetRailCarCurrentStatusByLocationandCarCount(int location, int car)
        {
            return this.ObjectContext.RailCarCurrentStatus.Where(y => y.LocationID == location && y.CarID == car).Count();
        }
       
        [Invoke]
        public int GetRailCarCurrentStatusCountLocation(int location, int car)
        {
            return this.ObjectContext.RailCarCurrentStatus.Where(y => y.LocationID == location && y.CarID == car && y.ShipDate == null).Count();
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
        public IQueryable<RailYard> GetRailYardsByLocation(int location)
        {
            return this.ObjectContext.RailYards.Where(y => y.LocationID == location);
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
        // To support paging you will need to add ordering to the 'Tracks' query.
        public IQueryable<Track> GetTracks()
        {
            return this.ObjectContext.Tracks;
        }
        public IQueryable<Track> GetTracksByLocation(int location)
        {
            return this.ObjectContext.Tracks.Include("RailYard").Include("TrackType").Where(y => y.LocationID == location);
        }
        public IQueryable<Track> GetTracksByYard(int yard)
        {
            return this.ObjectContext.Tracks.Include("RailYard").Include("TrackType").Where(y => y.YardId == yard);
        }
        [Invoke]
        public int GetTracksByNameandLocation(int location, string track)
        {
            return this.ObjectContext.Tracks.Where(y => y.LocationID == location && y.Track1 == track).Count();
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

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'TrackTypes' query.
        public IQueryable<TrackType> GetTrackTypes()
        {
            return this.ObjectContext.TrackTypes;
        }

        public void InsertTrackType(TrackType trackType)
        {
            if ((trackType.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(trackType, EntityState.Added);
            }
            else
            {
                this.ObjectContext.TrackTypes.AddObject(trackType);
            }
        }

        public void UpdateTrackType(TrackType currentTrackType)
        {
            this.ObjectContext.TrackTypes.AttachAsModified(currentTrackType, this.ChangeSet.GetOriginal(currentTrackType));
        }

        public void DeleteTrackType(TrackType trackType)
        {
            if ((trackType.EntityState == EntityState.Detached))
            {
                this.ObjectContext.TrackTypes.Attach(trackType);
            }
            this.ObjectContext.TrackTypes.DeleteObject(trackType);
        }
    }
}


