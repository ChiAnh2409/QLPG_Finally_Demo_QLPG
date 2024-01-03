using QLPG.Models;
using QLPG.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace QLPG.Controllers
{
    public class ThanhVienController: Controller
    {
        private QLPG1Entities db = new QLPG1Entities(); 
        public ActionResult ThanhVien() 
        {
            
            var list = new MultipleData();
            list.vien = db.ThanhVien.ToList();
            return View(list);
        }
        public ActionResult ThemDK()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ThemDK(ThanhVien tv)
        {
            if (ModelState.IsValid)
            {
                // Kiểm tra xem tên có chứa số không
                if (tv.TenTV.Any(char.IsDigit))
                {
                    ModelState.AddModelError("TenTV", "Tên không được chứa số.");
                }

                // Kiểm tra độ dài và định dạng số điện thoại
                if (tv.SDT.Length != 10 || !tv.SDT.All(char.IsDigit))
                {
                    ModelState.AddModelError("SDT", "Số điện thoại chỉ nhập đúng 10 số.");
                }

                // Kiểm tra định dạng email và chứa cả chữ và số
                if (!IsValidEmail(tv.Email) || !HasBothLettersAndNumbers(tv.Email))
                {
                    ModelState.AddModelError("Email", "Email không hợp lệ.");
                }

                if (ModelState.IsValid)
                {
                    // Nếu tất cả các điều kiện đều đúng, thêm vào cơ sở dữ liệu
                    DateTime now = DateTime.Now;
                    tv.NgayTao = now;
                    db.ThanhVien.Add(tv);
                    db.SaveChanges();

                    TempData["SuccessMessage"] = "Đăng ký thành công!";
                }
            }

            // Trả lại cùng view ThemDK (tức là view hiện tại) với các thông báo lỗi nếu có
            return View("ThemDK", tv);
        }

        // Hàm kiểm tra định dạng email
        // Hàm kiểm tra định dạng email
        private bool IsValidEmail(string email)
        {
            // Kiểm tra định dạng email và kết thúc bằng '@gmail.com'
            string emailRegex = @"^[a-zA-Z0-9_.+-]+@gmail\.com$";

            // Kiểm tra email có chứa ít nhất một chữ và ít nhất một số trước '@gmail.com'
            bool hasLettersAndNumbersBeforeGmail = email.Split('@')[0].Any(char.IsLetter) && email.Split('@')[0].Any(char.IsDigit);

            return Regex.IsMatch(email, emailRegex) && hasLettersAndNumbersBeforeGmail;
        }


        // Hàm kiểm tra email có chứa cả chữ và số
        private bool HasBothLettersAndNumbers(string input)
        {
            return input.Any(char.IsLetter) && input.Any(char.IsDigit);
        }

        public ActionResult ThemTV() 
        { 
            return View(); 
        }
        [HttpPost]
        public ActionResult ThemTV(ThanhVien tv)
        {
            DateTime now = DateTime.Now; 
            tv.NgayTao = now;
            db.ThanhVien.Add(tv);
            db.SaveChanges();
            return RedirectToAction("ThanhVien");
        }
        public ActionResult SuaTV(int id)
        {
            ThanhVien tv = db.ThanhVien.Find(id);
            return View(tv);
        }
        [HttpPost]
        public ActionResult SuaTV(ThanhVien tv)
        {
            //DateTime now = DateTime.Now;
            //tv.NgayTao = now;
            db.Entry(tv).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("ThanhVien");
        }
        [HttpPost]
        public ActionResult XoaTV(int id)
        {
            ThanhVien tv = db.ThanhVien.Find(id);
            db.ThanhVien.Remove(tv);
            db.SaveChanges();
            return RedirectToAction("ThanhVien");
        }
        [HttpPost]
        public ActionResult TimKiemTV(string search)
        {
            var list = new MultipleData();

            // Perform case-insensitive search based on TenTV or other properties as needed
            list.vien = db.ThanhVien
                           .Where(tv => tv.TenTV.ToLower().Contains(search.ToLower()))
                           .ToList();

            return View("ThanhVien", list);
        }

    }
}