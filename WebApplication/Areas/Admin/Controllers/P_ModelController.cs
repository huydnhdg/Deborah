using OfficeOpenXml;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication.Models;

namespace WebApplication.Areas.Admin.Controllers
{
    public class P_ModelController : BaseController
    {
        // GET: Admin/P_Model
        public ActionResult Index(int? page, string textsearch, string chanel, string status, string start_date, string end_date, string im_start_date, string im_end_date)
        {
            var model = from a in db.P_Model
                        select a;
            if (!string.IsNullOrEmpty(textsearch))
            {
                model = model.Where(a => a.Model.Contains(textsearch)
                || a.Trademark.Contains(textsearch)
                || a.Description.Contains(textsearch));
                ViewBag.textsearch = textsearch;
            }

            if (!string.IsNullOrEmpty(start_date))
            {
                DateTime s = DateTime.ParseExact(start_date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                model = model.Where(a => a.Createdate >= s);
                ViewBag.start_date = start_date;
            }
            if (!string.IsNullOrEmpty(end_date))
            {
                DateTime s = DateTime.ParseExact(end_date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                s = s.AddDays(1);
                model = model.Where(a => a.Createdate <= s);
                ViewBag.end_date = end_date;
            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(model.OrderByDescending(a => a.Createdate).ToPagedList(pageNumber, pageSize));
        }
        [HttpPost]
        public ActionResult Create()
        {
            return PartialView("~/Areas/Admin/Views/P_Model/_Create.cshtml", null);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateConfirm([Bind(Include = "")] P_Model cate)
        {
            if (ModelState.IsValid)
            {
                cate.Createby = User.Identity.Name;
                cate.Createdate = DateTime.Now;
                db.P_Model.Add(cate);
                db.SaveChanges();
                SetAlert("Đã lưu thông tin thành công.", "success");
                return RedirectToAction("Index");
            }
            SetAlert("Lưu thông tin không thành công. Hãy kiểm tra lại.", "danger");
            return RedirectToAction("Index");

        }
        [HttpPost]
        public ActionResult Edit(long Id)
        {
            var model = db.P_Model.Find(Id);
            return PartialView("~/Areas/Admin/Views/P_Model/_Edit.cshtml", model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditConfirm([Bind(Include = "")] P_Model cate)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cate).State = EntityState.Modified;
                db.SaveChanges();
                SetAlert("Đã lưu thông tin thành công.", "success");
                return RedirectToAction("Index");
            }
            SetAlert("Lưu thông tin không thành công. Hãy kiểm tra lại.", "danger");
            return RedirectToAction("Index");

        }
        public ActionResult Delete(long Id)
        {
            try
            {
                var model = db.P_Model.Find(Id);
                db.P_Model.Remove(model);
                db.SaveChanges();
                SetAlert("Đã xóa thành công.", "success");
            }
            catch (Exception ex)
            {
                SetAlert(ex.Message, "danger");
            }

            return RedirectToAction("Index");
        }

        public ActionResult UploadFile()
        {
            List<P_Model> list_product = new List<P_Model>();
            return View(list_product);
        }

        [HttpPost]
        public ActionResult UploadFile(FormCollection collection)
        {
            List<P_Model> list_product = new List<P_Model>();
            try
            {
                HttpPostedFileBase file = Request.Files["UploadedFile"];
                if ((file != null) && (file.ContentLength > 0) && !string.IsNullOrEmpty(file.FileName))
                {
                    string fileName = file.FileName;
                    string fileContentType = file.ContentType;
                    byte[] fileBytes = new byte[file.ContentLength];
                    var data = file.InputStream.Read(fileBytes, 0, Convert.ToInt32(file.ContentLength));
                    using (var package = new ExcelPackage(file.InputStream))
                    {
                        var currentSheet = package.Workbook.Worksheets;
                        var workSheet = currentSheet.First();
                        var noOfCol = workSheet.Dimension.End.Column;
                        var noOfRow = workSheet.Dimension.End.Row;
                        for (int rowIterator = 2; rowIterator <= noOfRow; rowIterator++)
                        {
                            string model;
                            string description;
                            string productname;
                            string limited = "";
                            string trademark;

                            try { model = workSheet.Cells[rowIterator, 1].Value.ToString(); } catch (Exception) { model = ""; }
                            try { productname = workSheet.Cells[rowIterator, 2].Value.ToString(); } catch (Exception) { productname = ""; }
                            try { limited = workSheet.Cells[rowIterator, 3].Value.ToString(); } catch (Exception) { limited = ""; }
                            try { trademark = workSheet.Cells[rowIterator, 4].Value.ToString(); } catch (Exception) { trademark = ""; }
                            try { description = workSheet.Cells[rowIterator, 5].Value.ToString(); } catch (Exception) { description = ""; }

                            //add thong tin rows vao product
                            var _limited = (!string.IsNullOrEmpty(limited)) ? int.Parse(limited) : 0;
                            var cate = new P_Model()
                            {
                                Model = model,
                                ProductName = productname,
                                Limited = _limited,
                                Description = description,
                                Trademark = trademark,
                                Createdate = DateTime.Now,
                                Createby = User.Identity.Name
                            };
                            //check trung serial code
                            if (!string.IsNullOrEmpty(model))
                            {
                                var _cate = db.P_Model.Where(a => a.Model == model);
                                if (_cate.Count() == 0)
                                {
                                    db.P_Model.Add(cate);
                                    db.SaveChanges();
                                }

                            }
                            list_product.Add(cate);
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return View(list_product);
        }
    }
}