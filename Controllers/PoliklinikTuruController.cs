using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using udemyWeb1.Haberlesme;
using udemyWeb1.Models;

namespace udemyWeb1.Controllers
{

    

    public class PoliklinikTuruController : Controller
    {
        private readonly IPoliklinikTuruRepository _poliklinikTuruRepository;

        public PoliklinikTuruController(IPoliklinikTuruRepository context)
        {
            _poliklinikTuruRepository = context;
        }
        public IActionResult Index()
        {
            List<PoliklinikTuru> objPoliklinikTuru = _poliklinikTuruRepository.GetAll().ToList();
            return View(objPoliklinikTuru);
        }
        public IActionResult Ekle() 
        {
            return View();
        }

        [HttpPost]
        public IActionResult Ekle(PoliklinikTuru poliklinikTuru)
        {
            if(ModelState.IsValid) {
                _poliklinikTuruRepository.Ekle(poliklinikTuru);
                _poliklinikTuruRepository.Kaydet();
                TempData["basarili"] = "Yeni Poliklinik Türü başarıyla oluşturuldu";
                return RedirectToAction("Index","PoliklinikTuru");
            }
            return View();
        }

        public IActionResult Guncelle(int? id)
        {
            if(id== null || id==0)
            {
                return NotFound();
            }
            PoliklinikTuru? poliklinikTuruVT = _poliklinikTuruRepository.Get(u=>u.Id==id);  //Expression<Func<T, bool>> filtre
            if(poliklinikTuruVT == null) 
            { 
                return NotFound(); 
            }
            return View(poliklinikTuruVT);
        }

        [HttpPost]
        public IActionResult Guncelle(PoliklinikTuru poliklinikTuru)
        {
            if (ModelState.IsValid)
            {
                _poliklinikTuruRepository.Guncelle(poliklinikTuru);
                _poliklinikTuruRepository.Kaydet();
                TempData["basarili"] = "Poliklinik Türü başarıyla güncellendi";
                return RedirectToAction("Index", "PoliklinikTuru");
            }
            return View();
        }

        public IActionResult Sil(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            PoliklinikTuru? poliklinikTuruVT = _poliklinikTuruRepository.Get(u => u.Id == id);
            if (poliklinikTuruVT == null)
            {
                return NotFound();
            }
            return View(poliklinikTuruVT);
        }

        [HttpPost,ActionName("Sil")]
        public IActionResult SilPOST(int? id)
        {
            PoliklinikTuru? poliklinikTuru = _poliklinikTuruRepository.Get(u => u.Id == id);
            if (poliklinikTuru == null)
            {
                return NotFound();
            }
            _poliklinikTuruRepository.Sil(poliklinikTuru);
            _poliklinikTuruRepository.Kaydet();
            TempData["basarili"] = "Poliklinik Türü başarıyla silindi";
            return RedirectToAction("index", "PoliklinikTuru");
        }
    }
}
