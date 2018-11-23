using System.IO;
using System.Web.Mvc;
using System.Collections.Generic;
using TrafficTicketsSystem1.Models;
using System.Runtime.Serialization.Formatters.Binary;

namespace TrafficTicketsSystem1.Controllers
{
    public class CamerasController : Controller
    {
        // GET: Cameras
        public ActionResult Index()
        {
            FileStream fsCameras = null;
            BinaryFormatter bfCameras = new BinaryFormatter();
            LinkedList<Camera> cameras = new LinkedList<Camera>();
            string strCamerasFile = Server.MapPath("~/App_Data/Cameras.tts");

            // If a file for Cameras records was proviously created, open it
            if (System.IO.File.Exists(strCamerasFile))
            {
                using (fsCameras = new FileStream(strCamerasFile, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    /* After opening the file, get the list of records from it
                     * and store in the declared LinkedList<> variable. */
                    cameras = (LinkedList<Camera>)bfCameras.Deserialize(fsCameras);
                }
            }

            return View(cameras);
        }

        // GET: Cameras/Details/
        public ActionResult Details()
        {
            return View();
        }

        // GET: Cameras/PrepareEdit/
        public ActionResult PrepareEdit()
        {
            return View();
        }

        // GET: Cameras/ConfirmEdit/
        public ActionResult ConfirmEdit(FormCollection collection)
        {
            if (!string.IsNullOrEmpty(collection["CameraNumber"]))
            {
                FileStream         fsCameras      = null;
                Camera             camera         = new Camera();
                BinaryFormatter    bfCameras      = new BinaryFormatter();
                LinkedList<Camera> cameras        = new LinkedList<Camera>();
                string             strCamerasFile = Server.MapPath("~/App_Data/Cameras.tts");

                if (System.IO.File.Exists(strCamerasFile))
                {
                    using (fsCameras = new FileStream(strCamerasFile, FileMode.Open, FileAccess.Read, FileShare.Read))
                    {
                        cameras = (LinkedList<Camera>)bfCameras.Deserialize(fsCameras);
                    }

                    camera.CameraNumber = collection["CameraNumber"];

                    LinkedListNode<Camera> viewer = cameras.FindLast(camera);

                    ViewBag.CameraNumber = collection["CameraNumber"];
                    ViewBag.Make = viewer.Value.Make;
                    ViewBag.Model = viewer.Value.Model;
                    ViewBag.Location = viewer.Value.Location;
                }
            }

            return View();
        }

        // GET: Cameras/Edit/
        public ActionResult Edit()
        {
            return View();
        }

        // POST: Cameras/Edit/
        [HttpPost]
        public ActionResult Edit(FormCollection collection)
        {
            try
            {
                FileStream fsCameras = null;
                Camera camera = new Camera();
                BinaryFormatter bfCameras = new BinaryFormatter();
                LinkedList<Camera> cameras = new LinkedList<Camera>();
                string strCamerasFile = Server.MapPath("~/App_Data/Cameras.tts");

                if (!string.IsNullOrEmpty("CameraNumber"))
                {
                    if (System.IO.File.Exists(strCamerasFile))
                    {
                        using (fsCameras = new FileStream(strCamerasFile, FileMode.Open, FileAccess.Read, FileShare.Read))
                        {
                            cameras = (LinkedList<Camera>)bfCameras.Deserialize(fsCameras);
                        }
                    }

                    camera.CameraNumber = collection["CameraNumber"];

                    LinkedListNode<Camera> viewer = cameras.FindLast(camera);

                    viewer.Value.Make = collection["Make"];
                    viewer.Value.Model = collection["Model"];
                    viewer.Value.Location = collection["Location"];

                    using (fsCameras = new FileStream(strCamerasFile, FileMode.Create, FileAccess.Write, FileShare.Write))
                    {
                        bfCameras.Serialize(fsCameras, cameras);
                    }
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Cameras/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Cameras/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                FileStream fsCameras = null;
                BinaryFormatter bfCameras = new BinaryFormatter();
                LinkedList<Camera> cameras = new LinkedList<Camera>();
                string strCamerasFile = Server.MapPath("~/App_Data/Cameras.tts");

                if (!string.IsNullOrEmpty("CameraNumber"))
                {
                    if (System.IO.File.Exists(strCamerasFile))
                    {
                        using (fsCameras = new FileStream(strCamerasFile, FileMode.Open, FileAccess.Read, FileShare.Read))
                        {
                            cameras = (LinkedList<Camera>)bfCameras.Deserialize(fsCameras);
                        }
                    }

                    Camera viewer = new Camera()
                    {
                        CameraNumber = collection["CameraNumber"],
                        Make = collection["Make"],
                        Model = collection["Model"],
                        Location = collection["Location"]
                    };

                    cameras.AddFirst(viewer);

                    using (fsCameras = new FileStream(strCamerasFile, FileMode.Create, FileAccess.Write, FileShare.Write))
                    {
                        bfCameras.Serialize(fsCameras, cameras);
                    }
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Cameras/PrepareDelete/
        public ActionResult PrepareDelete()
        {
            return View();
        }

        // GET: Cameras/ConfirmDelete/
        public ActionResult ConfirmDelete(FormCollection collection)
        {
            if (!string.IsNullOrEmpty(collection["CameraNumber"]))
            {
                FileStream fsCameras = null;
                Camera camera = new Camera();
                BinaryFormatter bfCameras = new BinaryFormatter();
                LinkedList<Camera> cameras = new LinkedList<Camera>();
                string strCamerasFile = Server.MapPath("~/App_Data/Cameras.tts");

                if (System.IO.File.Exists(strCamerasFile))
                {
                    using (fsCameras = new FileStream(strCamerasFile, FileMode.Open, FileAccess.Read, FileShare.Read))
                    {
                        cameras = (LinkedList<Camera>)bfCameras.Deserialize(fsCameras);
                    }

                    camera.CameraNumber = collection["CameraNumber"];

                    LinkedListNode<Camera> viewer = cameras.FindLast(camera);

                    ViewBag.CameraNumber = collection["CameraNumber"];
                    ViewBag.Make = viewer.Value.Make;
                    ViewBag.Model = viewer.Value.Model;
                    ViewBag.Location = viewer.Value.Location;
                }
            }

            return View();
        }

        // GET: Cameras/Delete/
        public ActionResult Delete()
        {
            return View();
        }

        // POST: Cameras/Delete/
        [HttpPost]
        public ActionResult Delete(FormCollection collection)
        {
            try
            {
                FileStream fsCameras = null;
                BinaryFormatter bfCameras = new BinaryFormatter();
                LinkedList<Camera> cameras = new LinkedList<Camera>();
                string strCamerasFile = Server.MapPath("~/App_Data/Cameras.tts");

                if (!string.IsNullOrEmpty("CameraNumber"))
                {
                    if (System.IO.File.Exists(strCamerasFile))
                    {
                        using (fsCameras = new FileStream(strCamerasFile, FileMode.Open, FileAccess.Read, FileShare.Read))
                        {
                            cameras = (LinkedList<Camera>)bfCameras.Deserialize(fsCameras);
                        }
                    }

                    // If a file for cameras exists, ...
                    if (cameras.Count > 0)
                    {
                        // ... create a Camera object ...
                        Camera camera = new Camera();
                        /* ... that is equivalent to the Camera that uses the 
                         * camera number that the user typed in the form */
                        camera.CameraNumber = collection["CameraNumber"];

                        // Now that you have identified that camera, delete it
                        cameras.Remove(camera);

                        // After updating the list of cameras, save the file
                        using (fsCameras = new FileStream(strCamerasFile, FileMode.Create, FileAccess.Write, FileShare.Write))
                        {
                            bfCameras.Serialize(fsCameras, cameras);
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