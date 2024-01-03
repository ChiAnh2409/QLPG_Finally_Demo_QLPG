using QLPG.Models;
using QLPG.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLPG.Controllers
{
    public class DangkyGoiTapController : Controller
    {
        private QLPG1Entities db = new QLPG1Entities();
        //tạo biến database để lấy dữ liệu
        // GET: DangkyGoiTap
        public ActionResult DKGT()
        {
            var list = new MultipleData();
            list.chiTietDK_ = db.ChiTietDK_GoiTap.Include("GoiTap").Include("HoiVien").ToList();
            list.goiTap = db.GoiTap.ToList();
            list.vien = db.ThanhVien.ToList();
            list.hoiViens = db.HoiVien.ToList();
            list.buoiTaps = db.BuoiTap.ToList(); // Lấy thông tin từ bảng BuoiTap
            return View(list);
        }
        public ActionResult LichTap(string search)
        {
            var list = new MultipleData();
            list.chiTietDK_ = db.ChiTietDK_GoiTap.Include("GoiTap").Include("HoiVien").ToList();
            list.goiTap = db.GoiTap.ToList();
            list.vien = db.ThanhVien.ToList();
            list.hoiViens = db.HoiVien.ToList();
            list.buoiTaps = db.BuoiTap.ToList(); // Lấy thông tin từ bảng BuoiTap

            // Apply search filter
            if (!string.IsNullOrEmpty(search))
            {
                search = search.ToLower().Trim();
                list.chiTietDK_ = list.chiTietDK_.Where(item =>
                    item.GoiTap.TenGoiTap.ToLower().Contains(search) ||
                    item.HoiVien.ThanhVien.TenTV.ToLower().Contains(search)
                ).ToList();
            }

            return View(list);
        }

        public ActionResult ThemDKGT()
        {
            var list = new MultipleData();
            list.chiTietDK_ = db.ChiTietDK_GoiTap.Include("GoiTap");
            list.chiTietDK_ = db.ChiTietDK_GoiTap.Include("HoiVien");
            list.goiTap = db.GoiTap.ToList();
            list.hoiViens = db.HoiVien.ToList();
            return View(list);
        }
        [HttpPost]
        public ActionResult ThemDKGT(ChiTietDK_GoiTap dkgt)
        {
            db.ChiTietDK_GoiTap.Add(dkgt);
            db.SaveChanges();
            return RedirectToAction("DKGT");
        }
        public ActionResult SuaDKGT(int id)
        {
            var viewmodel = new MultipleData();
            viewmodel.chiTietDK_ = db.ChiTietDK_GoiTap.Where(dkgt => dkgt.id_CTDKGoiTap == id).ToList();
            viewmodel.goiTap = db.GoiTap.ToList();
            viewmodel.hoiViens = db.HoiVien.ToList();
            return View(viewmodel);
        }
        [HttpPost]
        public ActionResult SuaDKGT(ChiTietDK_GoiTap dkgt)
        {
            var existingDangkyGoiTap = db.ChiTietDK_GoiTap.FirstOrDefault(item => item.id_CTDKGoiTap == dkgt.id_CTDKGoiTap);
            if (existingDangkyGoiTap != null)
            {
                existingDangkyGoiTap.id_GT = dkgt.id_GT;
                existingDangkyGoiTap.id_HV = dkgt.id_HV;
                existingDangkyGoiTap.NgayBatDau = dkgt.NgayBatDau;
                existingDangkyGoiTap.NgayKetThuc = dkgt.NgayKetThuc;
                existingDangkyGoiTap.ThanhTien = dkgt.ThanhTien;

                db.SaveChanges();
            }

            return RedirectToAction("DKGT");
        }
        public ActionResult XoaDKGT(int id)
        {
            var DangkyGoiTap = db.ChiTietDK_GoiTap.Find(id);
            if (DangkyGoiTap != null)
            {
                db.ChiTietDK_GoiTap.Remove(DangkyGoiTap);
                db.SaveChanges();

            }
            return RedirectToAction("DKGT");
        }
        [HttpPost]
        public ActionResult TimKiemDKGT(string search)
        {
            var list = new MultipleData();

            // Tìm kiếm theo tên thành viên trong bảng hội viên
            var hoiViensResults = db.HoiVien.Where(hv => hv.ThanhVien.TenTV.Contains(search)).ToList();
            var hoiVienIds = hoiViensResults.Select(hv => hv.id_HV).ToList();

            // Lấy danh sách ChiTietDK_GoiTap dựa trên các kết quả tìm kiếm
            list.chiTietDK_ = db.ChiTietDK_GoiTap
                .Include("HoiVien")
                .Include("GoiTap")
                .Where(dkgt => hoiVienIds.Contains(dkgt.HoiVien.id_HV))
                .ToList();

            list.goiTap = db.GoiTap.ToList();
            list.hoiViens = db.HoiVien.ToList();
            list.vien = db.ThanhVien.ToList();  //hiển thị thông báo
            ViewBag.Search = search; // Đặt tên cần tìm kiếm vào ViewBag để hiển thị trong view
            return View("DKGT", list);
        }
        //gia hạn gói tập cho hội viên
        [HttpGet]
        public ActionResult GiaHanGoiTap(int id_HV)
        {
            var expiredSubscription = db.ChiTietDK_GoiTap
                .Where(ct => ct.id_HV == id_HV && ct.NgayKetThuc < DateTime.Now)
                .OrderByDescending(ct => ct.NgayKetThuc)
                .FirstOrDefault();

            if (expiredSubscription != null)
            {
                var list = new MultipleData
                {
                    hoiViens = db.HoiVien.Where(hv => hv.id_HV == id_HV).ToList(),
                    chiTietDK_ = new List<ChiTietDK_GoiTap> { expiredSubscription }
                };

                return View(list);
            }

            return RedirectToAction("DKGT", "DangkyGoiTap");
        }

        [HttpPost]
        public ActionResult GiaHanGoiTap(int id_HV, int id_GT, DateTime NgayBatDau, DateTime NgayKetThuc, decimal ThanhTien)
        {
            var expiredSubscription = db.ChiTietDK_GoiTap
                .Where(ct => ct.id_GT == id_GT && ct.NgayKetThuc < DateTime.Now)
                .OrderByDescending(ct => ct.NgayKetThuc)
                .FirstOrDefault();

            if (expiredSubscription != null)
            {
                var newSubscription = new ChiTietDK_GoiTap
                {
                    id_GT = id_GT,
                    id_HV = id_HV,
                    NgayBatDau = NgayBatDau,
                    NgayKetThuc = NgayKetThuc,
                    ThanhTien = ThanhTien
                };

                db.ChiTietDK_GoiTap.Add(newSubscription);
                db.SaveChanges();

                return RedirectToAction("DKGT", "DangkyGoiTap");
            }

            return RedirectToAction("DKGT", "DangkyGoiTap");
        }
        [HttpGet]
        public ActionResult DiemDanhHV(int id_CTDKGoiTap, int id_HV)
        {
            var chiTietDK = db.ChiTietDK_GoiTap
                .Include("GoiTap")
                .Include("HoiVien.ThanhVien")
                .FirstOrDefault(d => d.id_CTDKGoiTap == id_CTDKGoiTap && d.HoiVien.id_HV == id_HV);

            if (chiTietDK != null)
            {
                // Check if the member has already attended for the specified id_CTDKGoiTap on the same day
                bool hasAttended = db.BuoiTap.Any(bt =>
                    bt.id_CTDKGoiTap == id_CTDKGoiTap &&
                    bt.NgayThamGia.HasValue &&
                    DbFunctions.TruncateTime(bt.NgayThamGia) == DateTime.Today &&
                    bt.DaDiemDanh == true);

                if (hasAttended)
                {
                    // Set flag to indicate that the member has attended
                    ViewBag.HasAttended = true;
                    return View(chiTietDK);
                }

                ViewBag.id_CTDKGoiTap = id_CTDKGoiTap;
                ViewBag.TenGoiTap = chiTietDK.GoiTap.TenGoiTap;
                ViewBag.TenHoiVien = chiTietDK.HoiVien.ThanhVien.TenTV;

                // Set flag to indicate that the member has not attended
                ViewBag.HasAttended = false;

                return View(chiTietDK);
            }

            return HttpNotFound();
        }

        [HttpPost]
        public ActionResult DiemDanhHV(int id_CTDKGoiTap, bool DaDiemDanh)
        {
            var existingAttendance = db.BuoiTap
                .FirstOrDefault(bt =>
                    bt.id_CTDKGoiTap == id_CTDKGoiTap &&
                    bt.NgayThamGia.HasValue &&
                    DbFunctions.TruncateTime(bt.NgayThamGia) == DateTime.Today);

            if (existingAttendance != null)
            {
                // Hội viên đã được điểm danh cho gói tập này trong ngày hôm nay.
                ViewBag.HasAttended = true;
                ViewBag.id_CTDKGoiTap = id_CTDKGoiTap; 
                return View("DiemDanhHV", existingAttendance.ChiTietDK_GoiTap);
            }

            var newAttendance = new BuoiTap
            {
                id_CTDKGoiTap = id_CTDKGoiTap,
                DaDiemDanh = DaDiemDanh,
                NgayThamGia = DateTime.Now
            };

            db.BuoiTap.Add(newAttendance);
            db.SaveChanges();

            ViewBag.HasAttended = true; // To display the success message
            ViewBag.id_CTDKGoiTap = id_CTDKGoiTap; //điểm danh theo id_dkgt riêng biệt 
            return RedirectToAction("LichTap");
        }
       
        public ActionResult CTDiemDanh(int id_CTDKGoiTap)
        {
            var list = new MultipleData();
            list.chiTietDK_ = db.ChiTietDK_GoiTap.Include("GoiTap").Include("HoiVien.ThanhVien").ToList();
            list.buoiTaps = db.BuoiTap
                .Where(bt => bt.ChiTietDK_GoiTap.id_CTDKGoiTap == id_CTDKGoiTap)
                .ToList();
            list.vien = db.ThanhVien.ToList();

            // Get the name of the member for the header
            var chiTietDK = list.chiTietDK_.FirstOrDefault(ct => ct.id_CTDKGoiTap == id_CTDKGoiTap);
            if (chiTietDK != null)
            {
                ViewBag.TenHoiVien = chiTietDK.HoiVien.ThanhVien.TenTV;
            }
            else
            {
                ViewBag.TenHoiVien = "Unknown Member";
            }

            //ViewBag.id_CTDKGoiTap = id_CTDKGoiTap;
            return View(list);
        }
        public ActionResult HuyGoiTap()
        {
            
            return View();
        }
       
    }

}