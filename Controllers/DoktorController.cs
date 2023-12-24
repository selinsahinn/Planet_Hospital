using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using udemyWeb1.Haberlesme;
using udemyWeb1.Models;

namespace udemyWeb1.Controllers
{

    

    public class DoktorController : Controller
    
    {
        private readonly IDoktorRepository _doktorRepository;
        private readonly IPoliklinikTuruRepository _poliklinikTuruRepository;
        public readonly IWebHostEnvironment _webHostEnvironment;

        public DoktorController(IDoktorRepository doktorRepository, IPoliklinikTuruRepository poliklinikTuruRepository, IWebHostEnvironment webHostEnvironment)
        {
            _doktorRepository = doktorRepository;
            _poliklinikTuruRepository = poliklinikTuruRepository;
            _webHostEnvironment = webHostEnvironment;

        }

        
        public IActionResult Index()
        {
            //List<Doktor> objDoktor = _doktorRepository.GetAll().ToList();
            List<Doktor> objDoktor = _doktorRepository.GetAll(includeProps:"PoliklinikTuru").ToList();

            return View(objDoktor);
        }

       
        public IActionResult EkleGuncelle(int? id) 
        {
            IEnumerable<SelectListItem> PoliklinikTuruList = _poliklinikTuruRepository.GetAll()
                .Select(k => new SelectListItem
                {
                    Text = k.Ad,
                    Value = k.Id.ToString()
                });
            ViewBag.PoliklinikTuruList = PoliklinikTuruList;

            if(id == null || id == 0)
            {
                //Ekleme
                return View();
            }
            else
            {
                //Güncelleme
                Doktor? doktorVT = _doktorRepository.Get(u => u.Id == id);  //Expression<Func<T, bool>> filtre
                if (doktorVT == null)
                {
                    return NotFound();
                }
                return View(doktorVT);
            }
            
        }

        [HttpPost]
        
        public IActionResult EkleGuncelle(Doktor doktor,IFormFile? file)
        {
            if(ModelState.IsValid) {

                string wwwRootPath = _webHostEnvironment.WebRootPath;
                string doktorPath=Path.Combine(wwwRootPath,@"img");


                if (file != null)
                {
                    using (var fileStream = new FileStream(Path.Combine(doktorPath, file.FileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    doktor.ResimUrl = @"\img\" + file.FileName;
                }

                if(doktor.Id == 0) 
                {
                    _doktorRepository.Ekle(doktor);
                    TempData["basarili"] = "Yeni Doktor başarıyla oluşturuldu";
                }
                else
                {
                    _doktorRepository.Guncelle(doktor);
                    TempData["basarili"] = "Doktor güncelleme başarılı ";
                }

                
                _doktorRepository.Kaydet();
                
                return RedirectToAction("Index","Doktor");
            }
            return View();
        }

        
        public IActionResult Sil(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Doktor? doktorVT = _doktorRepository.Get(u => u.Id == id);
            if (doktorVT == null)
            {
                return NotFound();
            }
            return View(doktorVT);
        }

        [HttpPost,ActionName("Sil")]
        
        public IActionResult SilPOST(int? id)
        {
            Doktor? doktor = _doktorRepository.Get(u => u.Id == id);
            if (doktor == null)
            {
                return NotFound();
            }
            _doktorRepository.Sil(doktor);
            _doktorRepository.Kaydet();
            TempData["basarili"] = "Doktor başarıyla silindi";
            return RedirectToAction("index", "Doktor");
        }
    }
}
