using System.IO;
using System.Web.Mvc;
using System.Collections.Generic;
using TrafficTicketsSystem1.Models;
using System.Runtime.Serialization.Formatters.Binary;

namespace TrafficTicketsSystem1.Controllers
{
    public class VehiclesController : Controller
    {
        // GET: Vehicles
        public ActionResult Index()
        {
            FileStream fsVehicles = null;
            BinaryFormatter bfVehicles = new BinaryFormatter();
            LinkedList<Vehicle> vehicles = new LinkedList<Vehicle>();

            string strVehiclesFile = Server.MapPath("~/App_Data/Vehicles.tts");

            if (System.IO.File.Exists(strVehiclesFile))
            {
                using (fsVehicles = new FileStream(strVehiclesFile, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    vehicles = (LinkedList<Vehicle>)bfVehicles.Deserialize(fsVehicles);
                }
            }

            return View(vehicles);
        }

        // GET: Vehicles/Details/
        public ActionResult Details()
        {
            return View();
        }

        // GET: Vehicles/PrepareEdit/
        public ActionResult PrepareEdit()
        {
            return View();
        }

        // GET: Vehicles/ConfirmEdit/
        public ActionResult ConfirmEdit(FormCollection collection)
        {
            if (!string.IsNullOrEmpty(collection["TagNumber"]))
            {
                FileStream fsVehicles = null;
                Vehicle vehicle = new Vehicle();
                BinaryFormatter bfVehicles = new BinaryFormatter();
                LinkedList<Vehicle> vehicles = new LinkedList<Vehicle>();
                string strVehiclesFile = Server.MapPath("~/App_Data/Vehicles.tts");

                if (System.IO.File.Exists(strVehiclesFile))
                {
                    using (fsVehicles = new FileStream(strVehiclesFile, FileMode.Open, FileAccess.Read, FileShare.Read))
                    {
                        vehicles = (LinkedList<Vehicle>)bfVehicles.Deserialize(fsVehicles);
                    }

                    vehicle.TagNumber = collection["TagNumber"];

                    LinkedListNode<Vehicle> car = vehicles.FindLast(vehicle);

                    ViewBag.TagNumber = collection["TagNumber"];
                    ViewBag.DrvLicNumber = car.Value.DrvLicNumber;
                    ViewBag.Make = car.Value.Make;
                    ViewBag.Model = car.Value.Model;
                    ViewBag.VehicleYear = car.Value.VehicleYear;
                    ViewBag.Color = car.Value.Color;
                }
            }

            return View();
        }

        // GET: Vehicles/Edit/
        public ActionResult Edit()
        {
            return View();
        }

        // POST: Vehicles/Edit/
        [HttpPost]
        public ActionResult Edit(FormCollection collection)
        {
            try
            {
                FileStream fsVehicles = null;
                Vehicle vehicle = new Vehicle();
                BinaryFormatter bfVehicles = new BinaryFormatter();
                LinkedList<Vehicle> vehicles = new LinkedList<Vehicle>();
                string strVehiclesFile = Server.MapPath("~/App_Data/Vehicles.tts");

                if (!string.IsNullOrEmpty("CameraNumber"))
                {
                    if (System.IO.File.Exists(strVehiclesFile))
                    {
                        using (fsVehicles = new FileStream(strVehiclesFile, FileMode.Open, FileAccess.Read, FileShare.Read))
                        {
                            vehicles = (LinkedList<Vehicle>)bfVehicles.Deserialize(fsVehicles);
                        }
                    }

                    vehicle.TagNumber = collection["TagNumber"];

                    LinkedListNode<Vehicle> car = vehicles.FindLast(vehicle);

                    car.Value.TagNumber = collection["TagNumber"];
                    car.Value.Make = collection["Make"];
                    car.Value.Model = collection["Model"];
                    car.Value.VehicleYear = collection["VehicleYear"];
                    car.Value.Color = collection["Color"];

                    using (fsVehicles = new FileStream(strVehiclesFile, FileMode.Create, FileAccess.Write, FileShare.Write))
                    {
                        bfVehicles.Serialize(fsVehicles, vehicles);
                    }
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Vehicles/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Vehicles/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                bool driverFound = false;
                FileStream fsVehicles = null, fsDrivers = null;
                BinaryFormatter bfDrivers = new BinaryFormatter();
                BinaryFormatter bfVehicles = new BinaryFormatter();
                LinkedList<Driver> drivers = new LinkedList<Driver>();
                LinkedList<Vehicle> vehicles = new LinkedList<Vehicle>();
                string strDriversFile = Server.MapPath("~/App_Data/Drivers.tts");
                string strVehiclesFile = Server.MapPath("~/App_Data/Vehicles.tts");

                // Open the file of the drivers records
                if (System.IO.File.Exists(strDriversFile))
                {
                    using (fsDrivers = new FileStream(strDriversFile, FileMode.Open, FileAccess.Read, FileShare.Read))
                    {
                        /* After opening the file, get the list of records from it
                         * and store in the empty LinkedList<> collection we started. */
                        drivers = (LinkedList<Driver>)bfDrivers.Deserialize(fsDrivers);

                        // Navigate to each record
                        foreach (Driver person in drivers)
                        {
                            /* When you get to a record, find out if its DrvLicNumber value 
                             * is the DrvLicNumber the user typed on the form. */
                            if (person.DrvLicNumber == collection["DrvLicNumber"])
                            {
                                /* If you find the record with that DrvLicNumber value, 
                                 * make a note to indicate it. */
                                driverFound = true;
                                break;
                            }
                        }
                    }
                }

                /* If the driverFound is true, it means the driver's license 
                 * number is valid, which means we can save the record. */
                if (driverFound == true)
                {
                    // Check whether a file for vehicles exists already.
                    if (System.IO.File.Exists(strVehiclesFile))
                    {
                        // If a file for vehicles exists already, open it ...
                        using (fsVehicles = new FileStream(strVehiclesFile, FileMode.Open, FileAccess.Read, FileShare.Read))
                        {
                            // ... and store the records in our vehicles variable
                            vehicles = (LinkedList<Vehicle>)bfVehicles.Deserialize(fsVehicles);
                        }
                    }

                    Vehicle car = new Vehicle()
                    {
                        TagNumber = collection["TagNumber"],
                        DrvLicNumber = collection["DrvLicNumber"],
                        Make = collection["Make"],
                        Model = collection["Model"],
                        VehicleYear = collection["VehicleYear"],
                        Color = collection["Color"]
                    };

                    LinkedListNode<Vehicle> node = new LinkedListNode<Vehicle>(car);
                    vehicles.AddLast(node);

                    using (fsVehicles = new FileStream(strVehiclesFile, FileMode.Create, FileAccess.Write, FileShare.Write))
                    {
                        bfVehicles.Serialize(fsVehicles, vehicles);
                    }
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Vehicles/PrepareDelete/
        public ActionResult PrepareDelete()
        {
            return View();
        }

        // GET: Vehicles/ConfirmDelete/
        public ActionResult ConfirmDelete(FormCollection collection)
        {
            if (!string.IsNullOrEmpty(collection["TagNumber"]))
            {
                FileStream fsVehicles = null;
                Vehicle car = new Vehicle();
                BinaryFormatter bfVehicles = new BinaryFormatter();
                LinkedList<Vehicle> vehicles = new LinkedList<Vehicle>();
                string strVehiclesFile = Server.MapPath("~/App_Data/Vehicles.tts");

                if (System.IO.File.Exists(strVehiclesFile))
                {
                    using (fsVehicles = new FileStream(strVehiclesFile, FileMode.Open, FileAccess.Read, FileShare.Read))
                    {
                        vehicles = (LinkedList<Vehicle>)bfVehicles.Deserialize(fsVehicles);
                    }

                    car.TagNumber = collection["TagNumber"];

                    LinkedListNode<Vehicle> node = vehicles.FindLast(car);

                    ViewBag.TagNumber = node.Value.TagNumber;
                    ViewBag.DrvLicNumber = node.Value.DrvLicNumber;
                    ViewBag.Make = node.Value.Make;
                    ViewBag.Model = node.Value.Model;
                    ViewBag.VehicleYear = node.Value.VehicleYear;
                    ViewBag.Color = node.Value.Color;
                }
            }

            return View();
        }

        // GET: Vehicles/Delete/
        public ActionResult Delete()
        {
            return View();
        }

        // POST: Drivers/Delete/
        [HttpPost]
        public ActionResult Delete(FormCollection collection)
        {
            try
            {
                FileStream fsVehicles = null;
                Vehicle car = new Vehicle();
                BinaryFormatter bfVehicles = new BinaryFormatter();
                LinkedList<Vehicle> vehicles = new LinkedList<Vehicle>();
                string strVehiclesFile = Server.MapPath("~/App_Data/Vehicles.tts");

                if (!string.IsNullOrEmpty("TagNumber"))
                {
                    if (System.IO.File.Exists(strVehiclesFile))
                    {
                        using (fsVehicles = new FileStream(strVehiclesFile, FileMode.Open, FileAccess.Read, FileShare.Read))
                        {
                            vehicles = (LinkedList<Vehicle>)bfVehicles.Deserialize(fsVehicles);
                        }
                    }

                    if (vehicles.Count > 0)
                    {
                        car.TagNumber = collection["TagNumber"];

                        //LinkedListNode<Vehicle> vehicle = vehicles.Find(car);

                        vehicles.Remove(car);

                        using (fsVehicles = new FileStream(strVehiclesFile, FileMode.Create, FileAccess.Write, FileShare.Write))
                        {
                            bfVehicles.Serialize(fsVehicles, vehicles);
                        }
                    }
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}