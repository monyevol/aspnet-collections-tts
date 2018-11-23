using System;
using System.IO;
using System.Web.Mvc;
using System.Collections.Generic;
using TrafficTicketsSystem1.Models;
using System.Runtime.Serialization.Formatters.Binary;

namespace TrafficTicketsSystem1.Controllers
{
    public class TrafficViolationsController : Controller
    {
        // GET: TrafficViolations
        public ActionResult Index()
        {
            FileStream fsTrafficViolations = null;
            BinaryFormatter bfTrafficViolations = new BinaryFormatter();
            LinkedList<TrafficViolation> violations = new LinkedList<TrafficViolation>();

            string strTrafficViolationsFile = Server.MapPath("~/App_Data/TrafficViolations.tts");

            if (System.IO.File.Exists(strTrafficViolationsFile))
            {
                using (fsTrafficViolations = new FileStream(strTrafficViolationsFile, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    violations = (LinkedList<TrafficViolation>)bfTrafficViolations.Deserialize(fsTrafficViolations);
                }
            }

            return View(violations);
        }

        // GET: Drivers/Details/
        public ActionResult Details()
        {
            return View();
        }

        // GET: TrafficViolations/PrepareEdit/
        public ActionResult PrepareEdit()
        {
            return View();
        }

        // GET: TrafficViolations/ConfirmEdit/
        public ActionResult ConfirmEdit(FormCollection collection)
        {
            FileStream fsTrafficViolations = null;
            BinaryFormatter bfTrafficViolations = new BinaryFormatter();
            LinkedList<TrafficViolation> violations = new LinkedList<TrafficViolation>();
            string strTrafficViolationsFile = Server.MapPath("~/App_Data/TrafficViolations.tts");

            if (System.IO.File.Exists(strTrafficViolationsFile))
            {
                using (fsTrafficViolations = new FileStream(strTrafficViolationsFile, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    violations = (LinkedList<TrafficViolation>)bfTrafficViolations.Deserialize(fsTrafficViolations);
                }

                TrafficViolation violation = new TrafficViolation();
                violation.TrafficViolationNumber = int.Parse(collection["TrafficViolationNumber"]);

                LinkedListNode<TrafficViolation> node = violations.FindLast(violation);

                BinaryFormatter bfCameras = new BinaryFormatter();
                FileStream fsCameras = null, fsViolationsTypes = null;
                LinkedList<Camera> cameras = new LinkedList<Camera>();
                BinaryFormatter bfViolationsTypes = new BinaryFormatter();
                string strCamerasFile = Server.MapPath("~/App_Data/Cameras.tts");
                LinkedList<ViolationType> violationsTypes = new LinkedList<ViolationType>();
                string strViolationsTypesFile = Server.MapPath("~/App_Data/ViolationsTypes.tts");

                List<SelectListItem> itemsCameras = new List<SelectListItem>();
                List<SelectListItem> paymentsStatus = new List<SelectListItem>();
                List<SelectListItem> itemsPhotoAvailable = new List<SelectListItem>();
                List<SelectListItem> itemsVideoAvailable = new List<SelectListItem>();
                List<SelectListItem> itemsViolationsTypes = new List<SelectListItem>();

                if (System.IO.File.Exists(strCamerasFile))
                {
                    using (fsCameras = new FileStream(strCamerasFile, FileMode.Open, FileAccess.Read, FileShare.Read))
                    {
                        cameras = (LinkedList<Camera>)bfCameras.Deserialize(fsCameras);

                        foreach (Camera cmr in cameras)
                        {
                            itemsCameras.Add(new SelectListItem() { Text = cmr.CameraNumber, Value = cmr.CameraNumber, Selected = (node.Value.CameraNumber == cmr.CameraNumber) });
                        }
                    }
                }

                if (System.IO.File.Exists(strViolationsTypesFile))
                {
                    using (fsViolationsTypes = new FileStream(strViolationsTypesFile, FileMode.Open, FileAccess.Read, FileShare.Read))
                    {
                        violationsTypes = (LinkedList<ViolationType>)bfViolationsTypes.Deserialize(fsViolationsTypes);

                        foreach (ViolationType vt in violationsTypes)
                        {
                            itemsViolationsTypes.Add(new SelectListItem() { Text = vt.ViolationName, Value = vt.ViolationName, Selected = (node.Value.ViolationName == vt.ViolationName) });
                        }
                    }
                }

                itemsPhotoAvailable.Add(new SelectListItem() { Value = "Unknown" });
                itemsPhotoAvailable.Add(new SelectListItem() { Text = "No", Value = "No", Selected = (node.Value.PhotoAvailable == "No") });
                itemsPhotoAvailable.Add(new SelectListItem() { Text = "Yes", Value = "Yes", Selected = (node.Value.PhotoAvailable == "Yes") });
                itemsVideoAvailable.Add(new SelectListItem() { Value = "Unknown" });
                itemsVideoAvailable.Add(new SelectListItem() { Text = "No", Value = "No", Selected = (node.Value.VideoAvailable == "No") });
                itemsVideoAvailable.Add(new SelectListItem() { Text = "Yes", Value = "Yes", Selected = (node.Value.VideoAvailable == "Yes") });

                paymentsStatus.Add(new SelectListItem() { Value = "Unknown" });
                paymentsStatus.Add(new SelectListItem() { Text = "Pending", Value = "Pending", Selected = (node.Value.PaymentStatus == "Pending") });
                paymentsStatus.Add(new SelectListItem() { Text = "Rejected", Value = "Rejected", Selected = (node.Value.PaymentStatus == "Rejected") });
                paymentsStatus.Add(new SelectListItem() { Text = "Cancelled", Value = "Cancelled", Selected = (node.Value.PaymentStatus == "Cancelled") });
                paymentsStatus.Add(new SelectListItem() { Text = "Paid Late", Value = "Paid Late", Selected = (node.Value.PaymentStatus == "Paid Late") });
                paymentsStatus.Add(new SelectListItem() { Text = "Paid On Time", Value = "Paid On Time", Selected = (node.Value.PaymentStatus == "Paid On Time") });

                ViewBag.CameraNumber = itemsCameras;
                ViewBag.PaymentStatus = paymentsStatus;
                ViewBag.TagNumber = node.Value.TagNumber;
                ViewBag.PaymentDate = node.Value.PaymentDate;
                ViewBag.ViolationName = itemsViolationsTypes;
                ViewBag.PhotoAvailable = itemsPhotoAvailable;
                ViewBag.VideoAvailable = itemsVideoAvailable;
                ViewBag.PaymentAmount = node.Value.PaymentAmount;
                ViewBag.ViolationDate = node.Value.ViolationDate;
                ViewBag.ViolationTime = node.Value.ViolationTime;
                ViewBag.PaymentDueDate = node.Value.PaymentDueDate;
                ViewBag.TrafficViolationNumber = node.Value.TrafficViolationNumber;
            }

            return View();
        }

        // GET: TrafficViolations/Edit/
        public ActionResult Edit()
        {
            return View();
        }

        // POST: TrafficViolations/Edit/
        [HttpPost]
        public ActionResult Edit(FormCollection collection)
        {
            try
            {
                FileStream fsTrafficViolations = null;
                TrafficViolation violation = new TrafficViolation();
                BinaryFormatter bfTrafficViolations = new BinaryFormatter();
                LinkedList<TrafficViolation> violations = new LinkedList<TrafficViolation>();
                string strTrafficViolationsFile = Server.MapPath("~/App_Data/TrafficViolations.tts");

                if (!string.IsNullOrEmpty("TrafficViolationNumber"))
                {
                    if (System.IO.File.Exists(strTrafficViolationsFile))
                    {
                        using (fsTrafficViolations = new FileStream(strTrafficViolationsFile, FileMode.Open, FileAccess.Read, FileShare.Read))
                        {
                            violations = (LinkedList<TrafficViolation>)bfTrafficViolations.Deserialize(fsTrafficViolations);
                        }
                    }

                    violation.TrafficViolationNumber = int.Parse(collection["TrafficViolationNumber"]);

                    LinkedListNode<TrafficViolation> infraction = violations.FindLast(violation);

                    infraction.Value.CameraNumber = collection["CameraNumber"];
                    infraction.Value.TagNumber = collection["TagNumber"];
                    infraction.Value.ViolationName = collection["ViolationName"];
                    infraction.Value.ViolationDate = collection["ViolationDate"];
                    infraction.Value.ViolationTime = collection["ViolationTime"];
                    infraction.Value.PhotoAvailable = collection["PhotoAvailable"];
                    infraction.Value.VideoAvailable = collection["VideoAvailable"];
                    infraction.Value.PaymentDueDate = collection["PaymentDueDate"];
                    infraction.Value.PaymentDate = collection["PaymentDate"];
                    infraction.Value.PaymentAmount = double.Parse(collection["PaymentAmount"]);
                    infraction.Value.PaymentStatus = collection["PaymentStatus"];

                    using (fsTrafficViolations = new FileStream(strTrafficViolationsFile, FileMode.Create, FileAccess.Write, FileShare.Write))
                    {
                        bfTrafficViolations.Serialize(fsTrafficViolations, violations);
                    }
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Drivers/PrepareCitation/
        public ActionResult PrepareCitation()
        {
            return View();
        }

        /* This function takes the available status of a photo and video taken during the traffic violation.
           The function returns a sentence that will instruct the driver about how to view the photo or video or the violation. */
        private string GetPhotoVideoOptions(string photo, string video)
        {
            var sentence = string.Empty;
            var pvAvailable = "No";

            if (photo == "Yes")
            {
                if (video == "Yes")
                {
                    pvAvailable = "Yes";
                    sentence = "To view the photo and/or video";
                }
                else
                {
                    pvAvailable = "Yes";
                    sentence = "To see a photo";
                }
            }
            else
            { // if (photo == "No")
                if (video == "Yes")
                {
                    pvAvailable = "Yes";
                    sentence = "To review a video";
                }
                else
                {
                    pvAvailable = "No";
                }
            }

            if (pvAvailable == "No")
                return "There is no photo or video available of this infraction but the violation was committed.";
            else
                return sentence + " of this violation, please access http://www.trafficviolationsmedia.com. In the form, enter the citation number and click Submit.";
        }

        // GET: Drivers/PresentCitation/
        public ActionResult PresentCitation(FormCollection collection)
        {
            FileStream fsTrafficViolations = null;
            BinaryFormatter bfTrafficViolations = new BinaryFormatter();
            LinkedList<TrafficViolation> violations = new LinkedList<TrafficViolation>();
            string strTrafficViolationsFile = Server.MapPath("~/App_Data/TrafficViolations.tts");

            if (System.IO.File.Exists(strTrafficViolationsFile))
            {
                using (fsTrafficViolations = new FileStream(strTrafficViolationsFile, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    violations = (LinkedList<TrafficViolation>)bfTrafficViolations.Deserialize(fsTrafficViolations);
                }

                TrafficViolation violation = new TrafficViolation();
                violation.TrafficViolationNumber = int.Parse(collection["TrafficViolationNumber"]);

                LinkedListNode<TrafficViolation> nodeTrafficViolation = violations.FindLast(violation);

                ViewBag.TrafficViolationNumber = collection["TrafficViolationNumber"];

                ViewBag.ViolationDate = nodeTrafficViolation.Value.ViolationDate;
                ViewBag.ViolationTime = nodeTrafficViolation.Value.ViolationTime;
                ViewBag.PaymentAmount = nodeTrafficViolation.Value.PaymentAmount.ToString("C");
                ViewBag.PaymentDueDate = nodeTrafficViolation.Value.PaymentDueDate;

                ViewBag.PhotoVideo = GetPhotoVideoOptions(nodeTrafficViolation.Value.PhotoAvailable, nodeTrafficViolation.Value.VideoAvailable);

                FileStream fsViolationsTypes = null;
                ViolationType category = new ViolationType();
                BinaryFormatter bfViolationsTypes = new BinaryFormatter();
                LinkedList<ViolationType> categories = new LinkedList<ViolationType>();
                string strViolationsTypesFile = Server.MapPath("~/App_Data/ViolationsTypes.tts");

                if (System.IO.File.Exists(strViolationsTypesFile))
                {
                    using (fsViolationsTypes = new FileStream(strViolationsTypesFile, FileMode.Open, FileAccess.Read, FileShare.Read))
                    {
                        categories = (LinkedList<ViolationType>)bfViolationsTypes.Deserialize(fsViolationsTypes);
                    }

                    category.ViolationName = nodeTrafficViolation.Value.ViolationName;

                    LinkedListNode<ViolationType> type = categories.Find(category);
                    ViewBag.ViolationName = nodeTrafficViolation.Value.ViolationName;
                    ViewBag.ViolationDescription = type.Value.Description;
                }

                FileStream fsVehicles = null;
                Vehicle car = new Vehicle();
                BinaryFormatter bfVehicles = new BinaryFormatter();
                LinkedList<Vehicle> vehicles = new LinkedList<Vehicle>();

                string strVehiclesFile = Server.MapPath("~/App_Data/Vehicles.tts");

                using (fsVehicles = new FileStream(strVehiclesFile, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    vehicles = (LinkedList<Vehicle>)bfVehicles.Deserialize(fsVehicles);
                }

                car.TagNumber = nodeTrafficViolation.Value.TagNumber;
                LinkedListNode<Vehicle> nodeVehicle = vehicles.FindLast(car);

                ViewBag.Vehicle = nodeVehicle.Value.Make + " " + nodeVehicle.Value.Model + ", " +
                                  nodeVehicle.Value.Color + ", " + nodeVehicle.Value.VehicleYear + " (Tag # " +
                                  nodeVehicle.Value.TagNumber + ")";

                FileStream fsDrivers = null;
                Driver driver = new Driver();
                BinaryFormatter bfDrivers = new BinaryFormatter();
                LinkedList<Driver> drivers = new LinkedList<Driver>();
                string strDriversFile = Server.MapPath("~/App_Data/Drivers.tts");

                using (fsDrivers = new FileStream(strDriversFile, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    drivers = (LinkedList<Driver>)bfDrivers.Deserialize(fsDrivers);
                }

                driver.DrvLicNumber = nodeVehicle.Value.DrvLicNumber;
                LinkedListNode<Driver> person = drivers.Find(driver);

                ViewBag.DriverName = person.Value.FirstName + " " + person.Value.LastName + " (Drv. Lic. # " + driver.DrvLicNumber + ")";
                ViewBag.DriverAddress = person.Value.Address + " " + person.Value.City + ", " + person.Value.State + " " + person.Value.ZIPCode;

                FileStream fsCameras = null;
                BinaryFormatter bfCameras = new BinaryFormatter();
                LinkedList<Camera> cameras = new LinkedList<Camera>();
                string strCamerasFile = Server.MapPath("~/App_Data/Cameras.tts");
                LinkedList<ViolationType> violationsTypes = new LinkedList<ViolationType>();

                using (fsCameras = new FileStream(strCamerasFile, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    cameras = (LinkedList<Camera>)bfCameras.Deserialize(fsCameras);
                }

                Camera camera = new Camera();

                camera.CameraNumber = nodeTrafficViolation.Value.CameraNumber;

                LinkedListNode<Camera> viewer = cameras.FindLast(camera);

                ViewBag.Camera = viewer.Value.Make + " " + viewer.Value.Model + " (" + viewer.Value.CameraNumber + ")";
                ViewBag.ViolationLocation = viewer.Value.Location;
            }

            return View();
        }

        // GET: TrafficViolations/Create
        public ActionResult Create()
        {
            Random rndNumber = new Random();
            BinaryFormatter bfCameras = new BinaryFormatter();
            FileStream fsCameras = null, fsViolationsTypes = null;
            LinkedList<Camera> cameras = new LinkedList<Camera>();
            BinaryFormatter bfViolationsTypes = new BinaryFormatter();
            string strCamerasFile = Server.MapPath("~/App_Data/Cameras.tts");
            LinkedList<ViolationType> violationsTypes = new LinkedList<ViolationType>();
            string strViolationsTypesFile = Server.MapPath("~/App_Data/ViolationsTypes.tts");

            List<SelectListItem> itemsCameras = new List<SelectListItem>();
            List<SelectListItem> paymentsStatus = new List<SelectListItem>();
            List<SelectListItem> itemsPhotoAvailable = new List<SelectListItem>();
            List<SelectListItem> itemsVideoAvailable = new List<SelectListItem>();
            List<SelectListItem> itemsViolationsTypes = new List<SelectListItem>();

            if (System.IO.File.Exists(strCamerasFile))
            {
                using (fsCameras = new FileStream(strCamerasFile, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    cameras = (LinkedList<Camera>)bfCameras.Deserialize(fsCameras);

                    foreach (Camera cmr in cameras)
                    {
                        itemsCameras.Add(new SelectListItem() { Text = cmr.CameraNumber + " - " + cmr.Location, Value = cmr.CameraNumber });
                    }
                }
            }

            if (System.IO.File.Exists(strViolationsTypesFile))
            {
                using (fsViolationsTypes = new FileStream(strViolationsTypesFile, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    violationsTypes = (LinkedList<ViolationType>)bfViolationsTypes.Deserialize(fsViolationsTypes);

                    foreach (ViolationType vt in violationsTypes)
                    {
                        itemsViolationsTypes.Add(new SelectListItem() { Text = vt.ViolationName, Value = vt.ViolationName });
                    }
                }
            }

            itemsPhotoAvailable.Add(new SelectListItem() { Value = "Unknown" });
            itemsPhotoAvailable.Add(new SelectListItem() { Text = "No", Value = "No" });
            itemsPhotoAvailable.Add(new SelectListItem() { Text = "Yes", Value = "Yes" });
            itemsVideoAvailable.Add(new SelectListItem() { Value = "Unknown" });
            itemsVideoAvailable.Add(new SelectListItem() { Text = "No", Value = "No" });
            itemsVideoAvailable.Add(new SelectListItem() { Text = "Yes", Value = "Yes" });

            paymentsStatus.Add(new SelectListItem() { Value = "Unknown" });
            paymentsStatus.Add(new SelectListItem() { Text = "Pending", Value = "Pending" });
            paymentsStatus.Add(new SelectListItem() { Text = "Rejected", Value = "Rejected" });
            paymentsStatus.Add(new SelectListItem() { Text = "Cancelled", Value = "Cancelled" });
            paymentsStatus.Add(new SelectListItem() { Text = "Paid Late", Value = "Paid Late" });
            paymentsStatus.Add(new SelectListItem() { Text = "Paid On Time", Value = "Paid On Time" });

            ViewBag.CameraNumber = itemsCameras;
            ViewBag.PaymentStatus = paymentsStatus;
            ViewBag.PhotoAvailable = itemsPhotoAvailable;
            ViewBag.VideoAvailable = itemsVideoAvailable;
            ViewBag.ViolationName = itemsViolationsTypes;
            ViewBag.TrafficViolationNumber = rndNumber.Next(10000001, 99999999);

            return View();
        }

        // POST: TrafficViolations/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                bool vehicleFound = false;
                BinaryFormatter bfDrivers = new BinaryFormatter();
                FileStream fsTrafficViolations = null, fsVehicles = null;
                LinkedList<Vehicle> vehicles = new LinkedList<Vehicle>();
                BinaryFormatter bfTrafficViolations = new BinaryFormatter();
                string strVehiclesFile = Server.MapPath("~/App_Data/Vehicles.tts");
                LinkedList<TrafficViolation> trafficViolations = new LinkedList<TrafficViolation>();
                string strTrafficViolationsFile = Server.MapPath("~/App_Data/TrafficViolations.tts");

                // Open the file for the vehicles
                if (System.IO.File.Exists(strVehiclesFile))
                {
                    using (fsVehicles = new FileStream(strVehiclesFile, FileMode.Open, FileAccess.Read, FileShare.Read))
                    {
                        /* After opening the file, get the list of vehicles from it
                         * and store it in the LinkedList<Vehicle> variable. */
                        vehicles = (LinkedList<Vehicle>)bfDrivers.Deserialize(fsVehicles);

                        // Check each record
                        foreach (Vehicle car in vehicles)
                        {
                            /* When you get to a record, find out if its TagNumber value 
                             * is the same as the tag number the user provided. */
                            if (car.TagNumber == collection["TagNumber"])
                            {
                                /* If you find a record with that tag number, make a note. */
                                vehicleFound = true;
                                break;
                            }
                        }
                    }
                }

                /* If the vehicleFound is true, it means the car that committed the infraction has been identified. */
                if (vehicleFound == true)
                {
                    // Check whether a file for traffic violations was created already.
                    if (System.IO.File.Exists(strTrafficViolationsFile))
                    {
                        // If a file for traffic violations exists already, open it ...
                        using (fsTrafficViolations = new FileStream(strTrafficViolationsFile, FileMode.Open, FileAccess.Read, FileShare.Read))
                        {
                            // ... and store the records in our trafficViolations variable
                            trafficViolations = (LinkedList<TrafficViolation>)bfTrafficViolations.Deserialize(fsTrafficViolations);
                        }
                    }

                    TrafficViolation tv = new TrafficViolation()
                    {
                        TrafficViolationNumber = int.Parse(collection["TrafficViolationNumber"]),
                        CameraNumber = collection["CameraNumber"].Substring(0, 9),
                        TagNumber = collection["TagNumber"],
                        ViolationName = collection["ViolationName"],
                        ViolationDate = collection["ViolationDate"],
                        ViolationTime = collection["ViolationTime"],
                        PhotoAvailable = collection["PhotoAvailable"],
                        VideoAvailable = collection["VideoAvailable"],
                        PaymentDueDate = collection["PaymentDueDate"],
                        PaymentDate = collection["PaymentDate"],
                        PaymentAmount = double.Parse(collection["PaymentAmount"]),
                        PaymentStatus = collection["PaymentStatus"]
                    };

                    // If a list of traffic violations exists already, get the last one 
                    LinkedListNode<TrafficViolation> node = trafficViolations.Last;

                    if (node == null)
                    {
                        trafficViolations.AddFirst(tv);
                    }
                    else
                    {
                        // If the file contains a node already, add the new one before the last
                        trafficViolations.AddBefore(node, tv);
                    }

                    // After updating the list of traffic violations, save the file 
                    using (fsTrafficViolations = new FileStream(strTrafficViolationsFile, FileMode.Create, FileAccess.Write, FileShare.Write))
                    {
                        bfTrafficViolations.Serialize(fsTrafficViolations, trafficViolations);
                    }
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: TrafficViolations/PrepareDelete/
        public ActionResult PrepareDelete()
        {
            return View();
        }

        // GET: TrafficViolations/ConfirmDelete/
        public ActionResult ConfirmDelete(FormCollection collection)
        {
            if (!string.IsNullOrEmpty(collection["TrafficViolationNumber"]))
            {
                FileStream fsTrafficViolations = null;
                TrafficViolation violation = new TrafficViolation();
                BinaryFormatter bfTrafficViolations = new BinaryFormatter();
                LinkedList<TrafficViolation> violations = new LinkedList<TrafficViolation>();
                string strTrafficViolationsFile = Server.MapPath("~/App_Data/TrafficViolations.tts");

                if (System.IO.File.Exists(strTrafficViolationsFile))
                {
                    using (fsTrafficViolations = new FileStream(strTrafficViolationsFile, FileMode.Open, FileAccess.Read, FileShare.Read))
                    {
                        violations = (LinkedList<TrafficViolation>)bfTrafficViolations.Deserialize(fsTrafficViolations);
                    }

                    violation.TrafficViolationNumber = int.Parse(collection["TrafficViolationNumber"]);

                    LinkedListNode<TrafficViolation> tv = violations.FindLast(violation);

                    ViewBag.TrafficViolationNumber = collection["TrafficViolationNumber"];
                    ViewBag.CameraNumber = tv.Value.CameraNumber;
                    ViewBag.TagNumber = tv.Value.TagNumber;
                    ViewBag.ViolationName = tv.Value.ViolationName;
                    ViewBag.ViolationDate = tv.Value.ViolationDate;
                    ViewBag.ViolationTime = tv.Value.ViolationTime;
                    ViewBag.PhotoAvailable = tv.Value.PhotoAvailable;
                    ViewBag.VideoAvailable = tv.Value.VideoAvailable;
                    ViewBag.PaymentDueDate = tv.Value.PaymentDueDate;
                    ViewBag.PaymentDate = tv.Value.PaymentDate;
                    ViewBag.PaymentAmount = tv.Value.PaymentAmount;
                    ViewBag.PaymentStatus = tv.Value.PaymentStatus;
                }
            }

            return View();
        }

        // GET: TrafficViolations/Delete/
        public ActionResult Delete()
        {
            return View();
        }

        // POST: TrafficViolations/Delete/
        [HttpPost]
        public ActionResult Delete(FormCollection collection)
        {
            try
            {
                FileStream fsTrafficViolations = null;
                TrafficViolation violation = new TrafficViolation();
                BinaryFormatter bfTrafficViolations = new BinaryFormatter();
                LinkedList<TrafficViolation> violations = new LinkedList<TrafficViolation>();
                string strTrafficViolationsFile = Server.MapPath("~/App_Data/TrafficViolations.tts");

                if (!string.IsNullOrEmpty("TrafficViolationNumber"))
                {
                    if (System.IO.File.Exists(strTrafficViolationsFile))
                    {
                        using (fsTrafficViolations = new FileStream(strTrafficViolationsFile, FileMode.Open, FileAccess.Read, FileShare.Read))
                        {
                            violations = (LinkedList<TrafficViolation>)bfTrafficViolations.Deserialize(fsTrafficViolations);
                        }
                    }

                    if (violations.Count > 0)
                    {
                        violation.TrafficViolationNumber = int.Parse(collection["TrafficViolationNumber"]);

                        LinkedListNode<TrafficViolation> tv = violations.FindLast(violation);

                        violations.Remove(violation);

                        using (fsTrafficViolations = new FileStream(strTrafficViolationsFile, FileMode.Create, FileAccess.Write, FileShare.Write))
                        {
                            bfTrafficViolations.Serialize(fsTrafficViolations, violations);
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