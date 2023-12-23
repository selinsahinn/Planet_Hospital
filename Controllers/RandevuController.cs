using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using udemyWeb1.Haberlesme;
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
        public IActionResult EkleGuncelle(Randevu randevu)  //resim eklenmiş olsa burda olcaktı vs
        {
            if(ModelState.IsValid) {   

                if(randevu.Id == 0) 
                {
                    _randevuRepository.Ekle(randevu);
                    TempData["basarili"] = "Yeni Randevu kaydı başarıyla oluşturuldu";
                }
                else
                {
                    _randevuRepository.Guncelle(randevu);
                    TempData["basarili"] = "Randevu kayıt güncelleme başarılı ";
                }


                _randevuRepository.Kaydet();
                
                return RedirectToAction("Index","Randevu");
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
