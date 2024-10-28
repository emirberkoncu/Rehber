using System;
using System.Collections.Generic;
using System.Linq;

namespace rehber
{
    class Program
    {
        static void Main(string[] args)
        {
            Rehber rehber = new Rehber();
            rehber.BaslangicVerileriEkle();

            bool devam = true;
            while (devam)
            {
                Console.WriteLine("Lütfen yapmak istediğiniz işlemi seçiniz :) ");
                Console.WriteLine("*******************************************");
                Console.WriteLine("(1) Yeni Numara Kaydetmek");
                Console.WriteLine("(2) Varolan Numarayı Silmek");
                Console.WriteLine("(3) Varolan Numarayı Güncelleme");
                Console.WriteLine("(4) Rehberi Listelemek");
                Console.WriteLine("(5) Rehberde Arama Yapmak");
                Console.WriteLine("(0) Çıkış Yapmak");

                string secim = Console.ReadLine();

                switch (secim)
                {
                    case "1":
                        rehber.NumaraEkle();
                        break;
                    case "2":
                        rehber.NumaraSil();
                        break;
                    case "3":
                        rehber.NumaraGuncelle();
                        break;
                    case "4":
                        rehber.RehberiListele();
                        break;
                    case "5":
                        rehber.RehberdeArama();
                        break;
                    case "0":
                        devam = false;
                        break;
                    default:
                        Console.WriteLine("Geçersiz bir seçim yaptınız. Tekrar deneyin.");
                        break;
                }
            }
        }
    }

    class Kisi
    {
        public string Isim { get; set; }
        public string Soyisim { get; set; }
        public string TelefonNumarasi { get; set; }

        public Kisi(string isim, string soyisim, string telefonNumarasi)
        {
            Isim = isim;
            Soyisim = soyisim;
            TelefonNumarasi = telefonNumarasi;
        }
    }

    class Rehber
    {
        private List<Kisi> kisiler = new List<Kisi>();

        public void BaslangicVerileriEkle()
        {
            kisiler.Add(new Kisi("Ali", "Yılmaz", "05330000001"));
            kisiler.Add(new Kisi("Ayşe", "Kara", "05330000002"));
            kisiler.Add(new Kisi("Mehmet", "Can", "05330000003"));
            kisiler.Add(new Kisi("Fatma", "Demir", "05330000004"));
            kisiler.Add(new Kisi("Ahmet", "Şahin", "05330000005"));
        }

        public void NumaraEkle()
        {
            Console.Write("Lütfen isim giriniz: ");
            string isim = Console.ReadLine();
            Console.Write("Lütfen soyisim giriniz: ");
            string soyisim = Console.ReadLine();
            Console.Write("Lütfen telefon numarası giriniz: ");
            string telefonNumarasi = Console.ReadLine();

            kisiler.Add(new Kisi(isim, soyisim, telefonNumarasi));
            Console.WriteLine("Kişi başarıyla eklendi.");
        }

        public void NumaraSil()
        {
            Console.Write("Lütfen numarasını silmek istediğiniz kişinin adını ya da soyadını giriniz: ");
            string arama = Console.ReadLine();

            Kisi kisi = kisiler.FirstOrDefault(k => k.Isim == arama || k.Soyisim == arama);

            if (kisi != null)
            {
                Console.WriteLine($"{kisi.Isim} isimli kişi rehberden silinmek üzere, onaylıyor musunuz? (y/n)");
                string onay = Console.ReadLine();
                if (onay.ToLower() == "y")
                {
                    kisiler.Remove(kisi);
                    Console.WriteLine("Kişi başarıyla silindi.");
                }
            }
            else
            {
                Console.WriteLine("Aradığınız kritere uygun veri rehberde bulunamadı.");
                Console.WriteLine("* Silmeyi sonlandırmak için : (1)");
                Console.WriteLine("* Yeniden denemek için      : (2)");
                if (Console.ReadLine() == "2")
                {
                    NumaraSil();
                }
            }
        }

        public void NumaraGuncelle()
        {
            Console.Write("Lütfen numarasını güncellemek istediğiniz kişinin adını ya da soyadını giriniz: ");
            string arama = Console.ReadLine();

            Kisi kisi = kisiler.FirstOrDefault(k => k.Isim == arama || k.Soyisim == arama);

            if (kisi != null)
            {
                Console.Write("Yeni telefon numarasını giriniz: ");
                string yeniNumara = Console.ReadLine();
                kisi.TelefonNumarasi = yeniNumara;
                Console.WriteLine("Kişi başarıyla güncellendi.");
            }
            else
            {
                Console.WriteLine("Aradığınız kritere uygun veri rehberde bulunamadı.");
                Console.WriteLine("* Güncellemeyi sonlandırmak için    : (1)");
                Console.WriteLine("* Yeniden denemek için              : (2)");
                if (Console.ReadLine() == "2")
                {
                    NumaraGuncelle();
                }
            }
        }

        public void RehberiListele()
        {
            Console.WriteLine("Listeleme sırasını seçiniz (1: A-Z, 2: Z-A): ");
            string siralamaSecimi = Console.ReadLine();

            List<Kisi> siraliKisiler = siralamaSecimi == "2" ?
                kisiler.OrderByDescending(k => k.Isim).ToList() :
                kisiler.OrderBy(k => k.Isim).ToList();

            Console.WriteLine("Telefon Rehberi");
            Console.WriteLine("**********************************************");
            foreach (var kisi in siraliKisiler)
            {
                Console.WriteLine($"İsim: {kisi.Isim}, Soyisim: {kisi.Soyisim}, Telefon Numarası: {kisi.TelefonNumarasi}");
            }
        }

        public void RehberdeArama()
        {
            Console.WriteLine("Arama yapmak istediğiniz tipi seçiniz.");
            Console.WriteLine("**********************************************");
            Console.WriteLine("(1) İsim veya soyisime göre arama yapmak");
            Console.WriteLine("(2) Telefon numarasına göre arama yapmak");
            string aramaTipi = Console.ReadLine();

            List<Kisi> aramaSonucu = new List<Kisi>();
            if (aramaTipi == "1")
            {
                Console.Write("Lütfen isim veya soyisim giriniz: ");
                string arama = Console.ReadLine();
                aramaSonucu = kisiler.Where(k => k.Isim.Contains(arama) || k.Soyisim.Contains(arama)).ToList();
            }
            else if (aramaTipi == "2")
            {
                Console.Write("Lütfen telefon numarası giriniz: ");
                string arama = Console.ReadLine();
                aramaSonucu = kisiler.Where(k => k.TelefonNumarasi.Contains(arama)).ToList();
            }

            if (aramaSonucu.Count > 0)
            {
                Console.WriteLine("Arama Sonuçlarınız:");
                Console.WriteLine("**********************************************");
                foreach (var kisi in aramaSonucu)
                {
                    Console.WriteLine($"İsim: {kisi.Isim}, Soyisim: {kisi.Soyisim}, Telefon Numarası: {kisi.TelefonNumarasi}");
                }
            }
            else
            {
                Console.WriteLine("Arama kriterlerine uygun veri bulunamadı.");
            }
        }
    }
}
