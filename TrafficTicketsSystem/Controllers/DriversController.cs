using System.IO;
using System.Web.Mvc;
using System.Collections.Generic;
using TrafficTicketsSystem1.Models;
using System.Runtime.Serialization.Formatters.Binary;

namespace TrafficTicketsSystem1.Controllers
{
    public class DriversController : Controller
    {
        // GET: Drivers
        public ActionResult Index()
        {
            FileStream fsDrivers = null;
            BinaryFormatter bfDrivers = new BinaryFormatter();
            LinkedList<Driver> drivers = new LinkedList<Driver>();
            string strDriversFile = Server.MapPath("~/App_Data/Drivers.tts");

            if (System.IO.File.Exists(strDriversFile))
            {
                using (fsDrivers = new FileStream(strDriversFile, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    drivers = (LinkedList<Driver>)bfDrivers.Deserialize(fsDrivers);
                }
            }

            return View(drivers);
        }

        // GET: Drivers/Details
        public ActionResult Details()
        {
            return View();
        }

        // GET: Drivers/PrepareEdit/
        public ActionResult PrepareEdit()
        {
            return View();
        }

        // GET: Drivers/ConfirmEdit/
        public ActionResult ConfirmEdit(FormCollection collection)
        {
            if (!string.IsNullOrEmpty(collection["DrvLicNumber"]))
            {
                FileStream fsDrivers       = null;
                Driver driver              = new Driver();
                BinaryFormatter bfDrivers  = new BinaryFormatter();
                LinkedList<Driver> drivers = new LinkedList<Driver>();
                string strDriversFile      = Server.MapPath("~/App_Data/Drivers.tts");

                if (System.IO.File.Exists(strDriversFile))
                {
                    using (fsDrivers = new FileStream(strDriversFile, FileMode.Open, FileAccess.Read, FileShare.Read))
                    {
                        drivers = (LinkedList<Driver>)bfDrivers.Deserialize(fsDrivers);
                    }

                    driver.DrvLicNumber = collection["DrvLicNumber"];

                    LinkedListNode<Driver> person = drivers.Find(driver);

                    ViewBag.DrvLicNumber = collection["DrvLicNumber"];
                    ViewBag.FirstName    = person.Value.FirstName;
                    ViewBag.LastName     = person.Value.LastName;
                    ViewBag.Address      = person.Value.Address;
                    ViewBag.City         = person.Value.City;
                    ViewBag.County       = person.Value.County;
                    ViewBag.State        = person.Value.State;
                    ViewBag.ZIPCode      = person.Value.ZIPCode;
                }
            }

            return View();
        }

        // GET: Drivers/Edit/
        public ActionResult Edit()
        {
            return View();
        }

        // POST: Drivers/Edit/
        [HttpPost]
        public ActionResult Edit(FormCollection collection)
        {
            try
            {
                FileStream fsDrivers       = null;
                Driver driver              = new Driver();
                BinaryFormatter bfDrivers  = new BinaryFormatter();
                LinkedList<Driver> drivers = new LinkedList<Driver>();
                string strDriversFile      = Server.MapPath("~/App_Data/Drivers.tts");

                if (!string.IsNullOrEmpty("CameraNumber"))
                {
                    if (System.IO.File.Exists(strDriversFile))
                    {
                        using (fsDrivers = new FileStream(strDriversFile, FileMode.Open, FileAccess.Read, FileShare.Read))
                        {
                            drivers = (LinkedList<Driver>)bfDrivers.Deserialize(fsDrivers);
                        }
                    }

                    driver.DrvLicNumber = collection["DrvLicNumber"];

                    LinkedListNode<Driver> person = drivers.Find(driver);

                    person.Value.FirstName = collection["FirstName"];
                    person.Value.LastName  = collection["LastName"];
                    person.Value.Address   = collection["Address"];
                    person.Value.City      = collection["City"];
                    person.Value.County    = collection["County"];
                    person.Value.State     = collection["State"];
                    person.Value.ZIPCode   = collection["ZIPCode"];

                    using (fsDrivers = new FileStream(strDriversFile, FileMode.Create, FileAccess.Write, FileShare.Write))
                    {
                        bfDrivers.Serialize(fsDrivers, drivers);
                    }
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Drivers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Drivers/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                Driver drv = new Driver();
                FileStream fsDrivers = null;
                BinaryFormatter bfDrivers = new BinaryFormatter();
                LinkedList<Driver> drivers = new LinkedList<Driver>();
                string strDriversFile = Server.MapPath("~/App_Data/Drivers.tts");

                // Make sure the user provides both the driver's license number and a state
                if ((!string.IsNullOrEmpty("DrvLicNumber")) && (!string.IsNullOrEmpty("State")))
                {
                    // Assuming the user provided a State value, make that
                    // Find out whether a file for drivers was created already
                    if (System.IO.File.Exists(strDriversFile))
                    {
                        // If that file exists, open it...
                        using (fsDrivers = new FileStream(strDriversFile, FileMode.Open, FileAccess.Read, FileShare.Read))
                        {
                            // ... and store the records in the LinkedList<Driver> variable
                            drivers = (LinkedList<Driver>)bfDrivers.Deserialize(fsDrivers);

                            // Check all the drivers records
                            foreach (Driver d in drivers)
                            {
                                // When you get to a record, if its State is the same the user enter, make it the reference node
                                if (d.State.Equals(collection["State"]))
                                {
                                    drv = d;
                                }
                            }
                        }
                    }

                    LinkedListNode<Driver> referenceNode = drivers.Find(drv);

                    // Prepare a driver's record to save
                    Driver person = new Driver()
                    {
                        DrvLicNumber = collection["DrvLicNumber"],
                        FirstName = collection["FirstName"],
                        LastName = collection["LastName"],
                        Address = collection["Address"],
                        City = collection["City"],
                        County = collection["County"],
                        State = collection["State"],
                        ZIPCode = collection["ZIPCode"],
                    };

                    // If there is no driver yet, then simply add the new driver as the last record
                    if (referenceNode == null)
                    {
                        drivers.AddLast(person);
                    }
                    else // if( referenceNode != null)
                    {
                        drivers.AddAfter(referenceNode, person);
                    }

                    using (fsDrivers = new FileStream(strDriversFile, FileMode.Create, FileAccess.Write, FileShare.Write))
                    {
                        bfDrivers.Serialize(fsDrivers, drivers);
                    }
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Drivers/PrepareDelete/
        public ActionResult PrepareDelete()
        {
            return View();
        }

        // GET: Drivers/ConfirmDelete/
        public ActionResult ConfirmDelete(FormCollection collection)
        {
            if (!string.IsNullOrEmpty(collection["DrvLicNumber"]))
            {
                FileStream fsDrivers = null;
                Driver driver = new Driver();
                BinaryFormatter bfDrivers = new BinaryFormatter();
                LinkedList<Driver> drivers = new LinkedList<Driver>();
                string strDriversFile = Server.MapPath("~/App_Data/Drivers.tts");

                if (System.IO.File.Exists(strDriversFile))
                {
                    using (fsDrivers = new FileStream(strDriversFile, FileMode.Open, FileAccess.Read, FileShare.Read))
                    {
                        drivers = (LinkedList<Driver>)bfDrivers.Deserialize(fsDrivers);
                    }

                    driver.DrvLicNumber = collection["DrvLicNumber"];

                    LinkedListNode<Driver> person = drivers.Find(driver);

                    ViewBag.DrvLicNumber = collection["DrvLicNumber"];
                    ViewBag.FirstName = person.Value.FirstName;
                    ViewBag.LastName = person.Value.LastName;
                    ViewBag.Address = person.Value.Address;
                    ViewBag.City = person.Value.City;
                    ViewBag.County = person.Value.County;
                    ViewBag.State = person.Value.State;
                    ViewBag.ZIPCode = person.Value.ZIPCode;
                }
            }

            return View();
        }

        // GET: Drivers/Delete/
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
                FileStream fsDrivers = null;
                Driver driver = new Driver();
                BinaryFormatter bfDrivers = new BinaryFormatter();
                LinkedList<Driver> drivers = new LinkedList<Driver>();
                string strDriversFile = Server.MapPath("~/App_Data/Drivers.tts");

                if (!string.IsNullOrEmpty("DrvLicNumber"))
                {
                    if (System.IO.File.Exists(strDriversFile))
                    {
                        using (fsDrivers = new FileStream(strDriversFile, FileMode.Open, FileAccess.Read, FileShare.Read))
                        {
                            drivers = (LinkedList<Driver>)bfDrivers.Deserialize(fsDrivers);
                        }
                    }

                    if (drivers.Count > 0)
                    {
                        driver.DrvLicNumber = collection["DrvLicNumber"];

                        LinkedListNode<Driver> viewer = drivers.Find(driver);

                        drivers.Remove(viewer);

                        using (fsDrivers = new FileStream(strDriversFile, FileMode.Create, FileAccess.Write, FileShare.Write))
                        {
                            bfDrivers.Serialize(fsDrivers, drivers);
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