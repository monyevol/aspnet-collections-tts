using System.IO;
using System.Web.Mvc;
using System.Collections.Generic;
using TrafficTicketsSystem1.Models;
using System.Runtime.Serialization.Formatters.Binary;

namespace TrafficTicketsSystem1.Controllers
{
    public class ViolationsTypesController : Controller
    {
        // GET: ViolationsTypes
        public ActionResult Index()
        {
            FileStream fsViolationsTypes = null;
            BinaryFormatter bfViolationsTypes = new BinaryFormatter();
            string strViolationsTypesFile = Server.MapPath("~/App_Data/ViolationsTypes.tts");
            LinkedList<ViolationType> categories = new LinkedList<ViolationType>();

            // If a file for violations types was proviously created, open it
            if (System.IO.File.Exists(strViolationsTypesFile))
            {
                using (fsViolationsTypes = new FileStream(strViolationsTypesFile, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    /* After opening the file, get the list of records from it
                     * and store in the declared LinkedList<> variable. */
                    categories = (LinkedList<ViolationType>)bfViolationsTypes.Deserialize(fsViolationsTypes);
                }
            }

            return View(categories);
        }

        // GET: ViolationsTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ViolationsTypes/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                FileStream fsViolationsTypes = null;
                LinkedList<ViolationType> categories = new LinkedList<ViolationType>();
                BinaryFormatter bfViolationsTypes = new BinaryFormatter();
                string strViolationsTypesFile = Server.MapPath("~/App_Data/ViolationsTypes.tts");

                if (!string.IsNullOrEmpty("DrvLicNumber"))
                {
                    if (System.IO.File.Exists(strViolationsTypesFile))
                    {
                        using (fsViolationsTypes = new FileStream(strViolationsTypesFile, FileMode.Open, FileAccess.Read, FileShare.Read))
                        {
                            categories = (LinkedList<ViolationType>)bfViolationsTypes.Deserialize(fsViolationsTypes);
                        }
                    }

                    ViolationType vt = new ViolationType()
                    {
                        ViolationName = collection["ViolationName"],
                        Description = collection["Description"],
                    };

                    categories.AddLast(vt);

                    using (fsViolationsTypes = new FileStream(strViolationsTypesFile, FileMode.Create, FileAccess.Write, FileShare.Write))
                    {
                        bfViolationsTypes.Serialize(fsViolationsTypes, categories);
                    }
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: ViolationsTypes/Details/
        public ActionResult Details()
        {
            return View();
        }

        // GET: ViolationsTypes/PrepareEdit/
        public ActionResult PrepareEdit()
        {
            return View();
        }

        // GET: ViolationsTypes/ConfirmEdit/
        public ActionResult ConfirmEdit(FormCollection collection)
        {
            if (!string.IsNullOrEmpty(collection["ViolationName"]))
            {
                FileStream fsViolationsTypes = null;
                ViolationType violationType = new ViolationType();
                BinaryFormatter bfViolationsTypes = new BinaryFormatter();
                LinkedList<ViolationType> violationsTypes = new LinkedList<ViolationType>();
                string strViolationsTypesFile = Server.MapPath("~/App_Data/ViolationsTypes.tts");

                if (System.IO.File.Exists(strViolationsTypesFile))
                {
                    using (fsViolationsTypes = new FileStream(strViolationsTypesFile, FileMode.Open, FileAccess.Read, FileShare.Read))
                    {
                        violationsTypes = (LinkedList<ViolationType>)bfViolationsTypes.Deserialize(fsViolationsTypes);
                    }

                    violationType.ViolationName = collection["ViolationName"];

                    LinkedListNode<ViolationType> vt = violationsTypes.Find(violationType);

                    ViewBag.ViolationName = collection["ViolationName"];
                    ViewBag.Description = vt.Value.Description;
                }
            }

            return View();
        }

        // GET: ViolationsTypes/Edit/
        public ActionResult Edit()
        {
            return View();
        }

        // POST: ViolationsTypes/Edit/
        [HttpPost]
        public ActionResult Edit(FormCollection collection)
        {
            try
            {
                FileStream fsViolationsTypes = null;
                ViolationType violationType = new ViolationType();
                BinaryFormatter bfViolationsTypes = new BinaryFormatter();
                LinkedList<ViolationType> ViolationsTypes = new LinkedList<ViolationType>();
                string strViolationsTypesFile = Server.MapPath("~/App_Data/ViolationsTypes.tts");

                if (!string.IsNullOrEmpty("ViolationName"))
                {
                    if (System.IO.File.Exists(strViolationsTypesFile))
                    {
                        using (fsViolationsTypes = new FileStream(strViolationsTypesFile, FileMode.Open, FileAccess.Read, FileShare.Read))
                        {
                            ViolationsTypes = (LinkedList<ViolationType>)bfViolationsTypes.Deserialize(fsViolationsTypes);
                        }
                    }

                    violationType.ViolationName = collection["ViolationName"];

                    LinkedListNode<ViolationType> vt = ViolationsTypes.Find(violationType);

                    vt.Value.Description = collection["Description"];

                    using (fsViolationsTypes = new FileStream(strViolationsTypesFile, FileMode.Create, FileAccess.Write, FileShare.Write))
                    {
                        bfViolationsTypes.Serialize(fsViolationsTypes, ViolationsTypes);
                    }
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: ViolationsTypes/PrepareDelete/
        public ActionResult PrepareDelete()
        {
            return View();
        }

        // GET: ViolationsTypes/ConfirmDelete/
        public ActionResult ConfirmDelete(FormCollection collection)
        {
            if (!string.IsNullOrEmpty(collection["ViolationName"]))
            {
                FileStream fsViolationsTypes = null;
                ViolationType violationType = new ViolationType();
                BinaryFormatter bfViolationsTypes = new BinaryFormatter();
                LinkedList<ViolationType> violationsTypes = new LinkedList<ViolationType>();
                string strViolationsTypesFile = Server.MapPath("~/App_Data/ViolationsTypes.tts");

                if (System.IO.File.Exists(strViolationsTypesFile))
                {
                    using (fsViolationsTypes = new FileStream(strViolationsTypesFile, FileMode.Open, FileAccess.Read, FileShare.Read))
                    {
                        violationsTypes = (LinkedList<ViolationType>)bfViolationsTypes.Deserialize(fsViolationsTypes);
                    }

                    violationType.ViolationName = collection["ViolationName"];

                    LinkedListNode<ViolationType> vt = violationsTypes.Find(violationType);

                    ViewBag.ViolationName = collection["ViolationName"];
                    ViewBag.Description = vt.Value.Description;
                }
            }

            return View();
        }

        // GET: ViolationsTypes/Delete/
        public ActionResult Delete()
        {
            return View();
        }

        // POST: ViolationsTypes/Delete/
        [HttpPost]
        public ActionResult Delete(FormCollection collection)
        {
            try
            {
                FileStream fsViolationsTypes = null;
                BinaryFormatter bfViolationsTypes = new BinaryFormatter();
                LinkedList<ViolationType> violationsTypes = new LinkedList<ViolationType>();
                string strViolationsTypesFile = Server.MapPath("~/App_Data/ViolationsTypes.tts");

                if (!string.IsNullOrEmpty("ViolationName"))
                {
                    if (System.IO.File.Exists(strViolationsTypesFile))
                    {
                        using (fsViolationsTypes = new FileStream(strViolationsTypesFile, FileMode.Open, FileAccess.Read, FileShare.Read))
                        {
                            violationsTypes = (LinkedList<ViolationType>)bfViolationsTypes.Deserialize(fsViolationsTypes);
                        }
                    }

                    if (violationsTypes.Count > 0)
                    {
                        ViolationType violationType = new ViolationType();

                        violationType.ViolationName = collection["ViolationName"];
                        violationsTypes.Remove(violationType);

                        using (fsViolationsTypes = new FileStream(strViolationsTypesFile, FileMode.Create, FileAccess.Write, FileShare.Write))
                        {
                            bfViolationsTypes.Serialize(fsViolationsTypes, violationsTypes);
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