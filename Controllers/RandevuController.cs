using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using udemyWeb1.Haberlesme;
using System;
using System.Linq.Expressions;
using udemyWeb1.Models;

namespace udemyWeb1.Controllers
{
    public class RandevuController : Controller
    
    {
        private readonly IRandevuRepository _randevuRepository;
        private readonly IDoktorRepository _doktorRepository;
        public readonly IWebHostEnvironment _webHostEnvironment;

        public RandevuController(IRandevuRepository randevuRepository, IDoktorRepository doktorRepository, IWebHostEnvironment webHostEnvironment)
        {
            _randevuRepository = randevuRepository;
            _doktorRepository = doktorRepository;
            _webHostEnvironment = webHostEnvironment;

        }

        public IActionResult Index()
        {
            List<Randevu> objRandevuList = _randevuRepository.GetAll(includeProps:"Doktor").ToList();
            return View(objRandevuList);
        }

        public IActionResult EkleGuncelle(int? id) 
        {
            IEnumerable<SelectListItem> DoktorList = _doktorRepository.GetAll()
                .Select(k => new SelectListItem
                {
                    Text = k.DoktorAdi,
                    Value = k.Id.ToString()
                });
            ViewBag.DoktorList = DoktorList;

            if(id == null || id == 0)
            {
                //Ekleme
                return View();
            }
            else
            {
                //Güncelleme
                Randevu? randevuVT = _randevuRepository.Get(u => u.Id == id);  //Expression<Func<T, bool>> filtre
                if (randevuVT == null)
                {
                    return NotFound();
                }
                return View(randevuVT);
            }
            
        }

        [HttpPost]
        public IActionResult EkleGuncelle(Randevu randevu)
        {
            if (ModelState.IsValid)
            {
                if (randevu.Id != 0)
                {
                    // Güncelleme senaryosu
                    Randevu eskiRandevu = _randevuRepository.Get(u => u.Id == randevu.Id);

                    // Eğer tarih veya saat değiştiyse kontrol yap
                    if (eskiRandevu.RandevuTarihi != randevu.RandevuTarihi || eskiRandevu.RandevuSaati != randevu.RandevuSaati)
                    {
                        // Aynı tarih ve saatte başka bir randevu var mı kontrol et
                        bool ayniTarihVeSaatteRandevuVar = _randevuRepository.Any(u =>
                            u.DoktorId == randevu.DoktorId &&
                            u.RandevuTarihi == randevu.RandevuTarihi &&
                            u.RandevuSaati == randevu.RandevuSaati &&
                            u.Id != randevu.Id); // Güncellenen randevuyu hariç tut

                        if (ayniTarihVeSaatteRandevuVar)
                        {
                            ModelState.AddModelError("RandevuTarihi", "Seçilen tarih ve saatte başka bir randevu bulunmaktadır.");
                            IEnumerable<SelectListItem> DoktorList = _doktorRepository.GetAll()
                                .Select(k => new SelectListItem
                                {
                                    Text = k.DoktorAdi,
                                    Value = k.Id.ToString()
                                });
                            ViewBag.DoktorList = DoktorList;
                            return View();
                        }
                    }
                }
                else
                {
                    // Yeni randevu ekleniyorsa, aynı tarih ve saatte başka bir randevu var mı kontrol et
                    bool ayniTarihVeSaatteYeniRandevuVar = _randevuRepository.Any(u =>
                        u.DoktorId == randevu.DoktorId &&
                        u.RandevuTarihi == randevu.RandevuTarihi &&
                        u.RandevuSaati == randevu.RandevuSaati);

                    if (ayniTarihVeSaatteYeniRandevuVar)
                    {
                        ModelState.AddModelError("RandevuTarihi", "Seçilen tarih ve saatte başka bir randevu bulunmaktadır.");
                        IEnumerable<SelectListItem> DoktorList = _doktorRepository.GetAll()
                            .Select(k => new SelectListItem
                            {
                                Text = k.DoktorAdi,
                                Value = k.Id.ToString()
                            });
                        ViewBag.DoktorList = DoktorList;
                        return View();
                    }
                }

                // Diğer kodlar...

                if (randevu.Id == 0)
                {
                    // Yeni randevu ekleme işlemi devam eder
                    _randevuRepository.Ekle(randevu);
                    TempData["basarili"] = "Yeni Randevu kaydı başarıyla oluşturuldu";
                }
                else
                {
                    // Güncelleme işlemi devam eder
                    _randevuRepository.Guncelle(randevu);
                    TempData["basarili"] = "Randevu kayıt güncelleme başarılı ";
                }

                _randevuRepository.Kaydet();

                return RedirectToAction("Index", "Randevu");
            }

            return View();
        }



        //GET ACTION
        public IActionResult Sil(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Randevu? randevuVT = _randevuRepository.Get(u => u.Id == id);
            if (randevuVT == null)
            {
                return NotFound();
            }
            return View(randevuVT);
        }

        [HttpPost,ActionName("Sil")]
        public IActionResult SilPOST(int? id)
        {
            Randevu? randevu = _randevuRepository.Get(u => u.Id == id);
            if (randevu == null)
            {
                return NotFound();
            }
            _randevuRepository.Sil(randevu);
            _randevuRepository.Kaydet();
            TempData["basarili"] = "Randevu kaydı başarıyla silindi";
            return RedirectToAction("index", "Randevu");
        }
    }
}
