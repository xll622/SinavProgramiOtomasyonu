using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginModulForm
{
    public class SinavProgramiMotoru
    {
        private readonly string connectionString;
        private readonly int bolumID;

        public SinavProgramiMotoru(string connectionString, int bolumID)
        {
            this.connectionString = connectionString;
            this.bolumID = bolumID;
        }

        public MotorSonuc ProgramlariOlustur(int takvimID, bool cumartesiKullanilsin)
        {
            MotorSonuc sonuc = new MotorSonuc();

            try
            {
                List<DersBilgi> dersler = DersleriGetir();
                List<ZamanBilgi> zamanlar = ZamanlariGetir(takvimID, cumartesiKullanilsin);
                List<DerslikBilgi> derslikler = DerslikleriGetir();

                string kontrolMesaji = TemelKontrol(dersler, zamanlar, derslikler);

                if (!string.IsNullOrWhiteSpace(kontrolMesaji))
                {
                    sonuc.Basarili = false;
                    sonuc.Mesaj = kontrolMesaji;
                    return sonuc;
                }

                List<string> hatalar = new List<string>();

                List<SinavAtama> program1 = ProgramOlusturBellekte(dersler, zamanlar, derslikler, 1, hatalar);
                List<SinavAtama> program2 = ProgramOlusturBellekte(dersler, zamanlar, derslikler, 2, hatalar);
                List<SinavAtama> program3 = ProgramOlusturBellekte(dersler, zamanlar, derslikler, 3, hatalar);

                if (program1 == null || program2 == null || program3 == null)
                {
                    sonuc.Basarili = false;
                    sonuc.Mesaj = "Program oluşturulamadı. Bazı dersler uygun zaman veya derslik bulamadı.\n\n" +
                                  string.Join("\n", hatalar.Distinct().ToArray());
                    return sonuc;
                }

                List<SinavAtama> tumAtamalar = new List<SinavAtama>();
                tumAtamalar.AddRange(program1);
                tumAtamalar.AddRange(program2);
                tumAtamalar.AddRange(program3);

                ProgramlariKaydet(takvimID, tumAtamalar);

                sonuc.Basarili = true;
                sonuc.Mesaj = "3 farklı sınav programı başarıyla oluşturuldu.";
                return sonuc;
            }
            catch (Exception ex)
            {
                sonuc.Basarili = false;
                sonuc.Mesaj = "Program oluşturulurken hata oluştu:\n\n" + ex.Message;
                return sonuc;
            }
        }

        private string TemelKontrol(List<DersBilgi> dersler, List<ZamanBilgi> zamanlar, List<DerslikBilgi> derslikler)
        {
            if (dersler.Count == 0)
                return "Bu bölüme ait ders bulunamadı.";

            if (zamanlar.Count == 0)
                return "Seçilen akademik takvime ait uygun zaman dilimi bulunamadı.";

            if (derslikler.Count == 0)
                return "Sistemde kayıtlı derslik bulunamadı.";

            int enBuyukKapasite = derslikler.Max(x => x.Kapasite);

            List<DersBilgi> kapasiteSorunluDersler = dersler
                .Where(x => x.OgrenciSayisi > enBuyukKapasite)
                .ToList();

            if (kapasiteSorunluDersler.Count > 0)
            {
                string mesaj = "Bazı derslerin öğrenci sayısı en büyük derslik kapasitesinden fazla:\n\n";

                foreach (DersBilgi ders in kapasiteSorunluDersler)
                {
                    mesaj += ders.DersAdi + " | Öğrenci Sayısı: " + ders.OgrenciSayisi + "\n";
                }

                return mesaj;
            }

            return "";
        }

        private List<DersBilgi> DersleriGetir()
        {
            List<DersBilgi> dersler = new List<DersBilgi>();

            string query = @"
                SELECT
                    d.DersID,
                    d.DersAdi,
                    d.BolumID,
                    d.DersTipiID,
                    d.SinifSeviyeID,
                    d.Kredi,
                    d.SinavSuresi,
                    d.DersiAlanOgrenciSayisi,
                    dt.TipAd,
                    ss.SeviyeNo,
                    ss.SinifMevcudu
                FROM dbo.Ders d
                INNER JOIN dbo.DersTipi dt 
                    ON d.DersTipiID = dt.DersTipiID
                INNER JOIN dbo.SinifSeviyesi ss 
                    ON d.SinifSeviyeID = ss.SinifSeviyeID
                WHERE d.BolumID = @BolumID";

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.Add("@BolumID", SqlDbType.Int).Value = bolumID;

                conn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        DersBilgi ders = new DersBilgi();

                        ders.DersID = Convert.ToInt32(dr["DersID"]);
                        ders.DersAdi = dr["DersAdi"].ToString();
                        ders.BolumID = Convert.ToInt32(dr["BolumID"]);
                        ders.DersTipiID = Convert.ToInt32(dr["DersTipiID"]);
                        ders.SinifSeviyeID = Convert.ToInt32(dr["SinifSeviyeID"]);
                        ders.Kredi = Convert.ToInt32(dr["Kredi"]);
                        ders.SinavSuresi = Convert.ToInt32(dr["SinavSuresi"]);
                        ders.OgrenciSayisi = Convert.ToInt32(dr["DersiAlanOgrenciSayisi"]);
                        ders.TipAd = dr["TipAd"].ToString();
                        ders.SeviyeNo = Convert.ToInt32(dr["SeviyeNo"]);
                        ders.SinifMevcudu = Convert.ToInt32(dr["SinifMevcudu"]);

                        dersler.Add(ders);
                    }
                }
            }

            return dersler;
        }

        private List<ZamanBilgi> ZamanlariGetir(int takvimID, bool cumartesiKullanilsin)
        {
            List<ZamanBilgi> zamanlar = new List<ZamanBilgi>();

            string query = @"
                SELECT
                    ZamanID,
                    TakvimID,
                    Tarih,
                    BaslangicSaat,
                    BitisSaat
                FROM dbo.ZamanDilimi
                WHERE TakvimID = @TakvimID
                ORDER BY Tarih, BaslangicSaat";

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.Add("@TakvimID", SqlDbType.Int).Value = takvimID;

                conn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        DateTime tarih = Convert.ToDateTime(dr["Tarih"]).Date;

                        if (tarih.DayOfWeek == DayOfWeek.Sunday)
                            continue;

                        if (!cumartesiKullanilsin && tarih.DayOfWeek == DayOfWeek.Saturday)
                            continue;

                        ZamanBilgi zaman = new ZamanBilgi();

                        zaman.ZamanID = Convert.ToInt32(dr["ZamanID"]);
                        zaman.TakvimID = Convert.ToInt32(dr["TakvimID"]);
                        zaman.Tarih = tarih;
                        zaman.BaslangicSaat = DegeriTimeSpanYap(dr["BaslangicSaat"]);
                        zaman.BitisSaat = DegeriTimeSpanYap(dr["BitisSaat"]);

                        zamanlar.Add(zaman);
                    }
                }
            }

            return zamanlar;
        }

        private List<DerslikBilgi> DerslikleriGetir()
        {
            List<DerslikBilgi> derslikler = new List<DerslikBilgi>();

            string query = @"
                SELECT
                    DerslikID,
                    DerslikAd,
                    Kapasite
                FROM dbo.Derslik
                ORDER BY Kapasite DESC";

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                conn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        DerslikBilgi derslik = new DerslikBilgi();

                        derslik.DerslikID = Convert.ToInt32(dr["DerslikID"]);
                        derslik.DerslikAd = dr["DerslikAd"].ToString();
                        derslik.Kapasite = Convert.ToInt32(dr["Kapasite"]);

                        derslikler.Add(derslik);
                    }
                }
            }

            return derslikler;
        }

        private List<SinavAtama> ProgramOlusturBellekte(
            List<DersBilgi> dersler,
            List<ZamanBilgi> zamanlar,
            List<DerslikBilgi> derslikler,
            int programVersiyon,
            List<string> hatalar)
        {
            List<SinavAtama> atamalar = new List<SinavAtama>();

            List<DersBilgi> siraliDersler = DersleriSirala(dersler, programVersiyon);
            List<ZamanBilgi> siraliZamanlar = ZamanlariSirala(zamanlar, programVersiyon);
            List<DerslikBilgi> siraliDerslikler = DerslikleriSirala(derslikler, programVersiyon);

            Random random = new Random(Environment.TickCount + programVersiyon * 1000);

            bool rektorlukBasarili = RektorlukDersleriniOzelYerlestir(
                siraliDersler,
                siraliZamanlar,
                siraliDerslikler,
                atamalar,
                programVersiyon,
                hatalar
            );

            if (!rektorlukBasarili)
                return null;

            bool havuzBasarili = HavuzDersleriniOzelYerlestir(
                siraliDersler,
                siraliZamanlar,
                siraliDerslikler,
                atamalar,
                programVersiyon,
                hatalar
            );

            if (!havuzBasarili)
                return null;

            foreach (DersBilgi ders in siraliDersler)
            {
                if (RektorlukDersiMi(ders))
                    continue;

                if (HavuzDersiMi(ders))
                    continue;

                List<AdayAtama> adaylar = new List<AdayAtama>();

                foreach (ZamanBilgi zaman in siraliZamanlar)
                {
                    int zamanDakika = Convert.ToInt32((zaman.BitisSaat - zaman.BaslangicSaat).TotalMinutes);

                    if (zamanDakika < ders.SinavSuresi)
                        continue;

                    foreach (DerslikBilgi derslik in siraliDerslikler)
                    {
                        if (derslik.Kapasite < ders.OgrenciSayisi)
                            continue;

                        if (AyniSinifAyniZamandaVarMi(atamalar, ders, zaman))
                            continue;

                        if (DerslikAyniZamandaDoluMu(atamalar, derslik, zaman))
                            continue;

                        if (BirinciSinifRektorlukGunuEngeliVarMi(atamalar, ders, zaman))
                            continue;

                        AdayAtama aday = new AdayAtama();
                        aday.Ders = ders;
                        aday.Zaman = zaman;
                        aday.Derslik = derslik;
                        aday.Puan = AtamaPuaniHesapla(
                            atamalar,
                            ders,
                            zaman,
                            derslik,
                            zamanlar,
                            programVersiyon,
                            random
                        );

                        adaylar.Add(aday);
                    }
                }

                if (adaylar.Count == 0)
                {
                    hatalar.Add("Yerleştirilemedi: " + ders.DersAdi + " | Sınıf: " + ders.SeviyeNo);
                    return null;
                }

                AdayAtama secilenAday = adaylar
                    .OrderBy(x => x.Puan)
                    .ThenBy(x => x.Derslik.Kapasite - x.Ders.OgrenciSayisi)
                    .First();

                SinavAtama atama = AtamaOlustur(secilenAday, programVersiyon);
                atamalar.Add(atama);
            }

            return atamalar;
        }

        private bool RektorlukDersleriniOzelYerlestir(
            List<DersBilgi> dersler,
            List<ZamanBilgi> zamanlar,
            List<DerslikBilgi> derslikler,
            List<SinavAtama> atamalar,
            int programVersiyon,
            List<string> hatalar)
        {
            List<DersBilgi> rektorlukDersleri = dersler
                .Where(x => RektorlukDersiMi(x))
                .OrderBy(x => RektorlukDersSirasi(x.DersAdi))
                .ThenBy(x => x.DersAdi)
                .ToList();

            if (rektorlukDersleri.Count == 0)
                return true;

            List<DateTime> persembeGunleri = zamanlar
                .Where(x => x.Tarih.DayOfWeek == DayOfWeek.Thursday)
                .Select(x => x.Tarih.Date)
                .Distinct()
                .OrderBy(x => x)
                .ToList();

            foreach (DateTime gun in persembeGunleri)
            {
                List<ZamanBilgi> gunSlotlari = zamanlar
                    .Where(x => x.Tarih.Date == gun.Date)
                    .OrderBy(x => x.BaslangicSaat)
                    .ToList();

                List<ZamanBilgi> ardArdaSlotlar = ArdArdaSlotlariBul(gunSlotlari, rektorlukDersleri.Count);

                if (ardArdaSlotlar == null)
                    continue;

                List<SinavAtama> geciciAtamalar = new List<SinavAtama>();

                bool buGuneYerlesti = true;

                for (int i = 0; i < rektorlukDersleri.Count; i++)
                {
                    DersBilgi ders = rektorlukDersleri[i];
                    ZamanBilgi zaman = ardArdaSlotlar[i];

                    int zamanDakika = Convert.ToInt32((zaman.BitisSaat - zaman.BaslangicSaat).TotalMinutes);

                    if (zamanDakika < ders.SinavSuresi)
                    {
                        buGuneYerlesti = false;
                        break;
                    }

                    DerslikBilgi derslik = UygunDerslikBul(
                        atamalar.Concat(geciciAtamalar).ToList(),
                        ders,
                        zaman,
                        derslikler
                    );

                    if (derslik == null)
                    {
                        buGuneYerlesti = false;
                        break;
                    }

                    AdayAtama aday = new AdayAtama();
                    aday.Ders = ders;
                    aday.Zaman = zaman;
                    aday.Derslik = derslik;
                    aday.Puan = 0;

                    geciciAtamalar.Add(AtamaOlustur(aday, programVersiyon));
                }

                if (buGuneYerlesti)
                {
                    atamalar.AddRange(geciciAtamalar);
                    return true;
                }
            }

            hatalar.Add("Rektörlük dersleri Perşembe günü ardışık saatlere yerleştirilemedi. Perşembe günü yeterli ardışık slot olmalı. Örn: 09-10, 10-11, 11-12.");
            return false;
        }

        private bool HavuzDersleriniOzelYerlestir(
            List<DersBilgi> dersler,
            List<ZamanBilgi> zamanlar,
            List<DerslikBilgi> derslikler,
            List<SinavAtama> atamalar,
            int programVersiyon,
            List<string> hatalar)
        {
            List<DersBilgi> havuzDersleri = dersler
                .Where(x => HavuzDersiMi(x))
                .OrderBy(x => x.SeviyeNo)
                .ThenBy(x => x.DersAdi)
                .ToList();

            if (havuzDersleri.Count == 0)
                return true;

            List<ZamanBilgi> carsambaOnOnBirSlotlari = zamanlar
                .Where(x =>
                    x.Tarih.DayOfWeek == DayOfWeek.Wednesday &&
                    x.BaslangicSaat == new TimeSpan(10, 0, 0) &&
                    x.BitisSaat == new TimeSpan(11, 0, 0))
                .OrderBy(x => x.Tarih)
                .ToList();

            if (carsambaOnOnBirSlotlari.Count == 0)
            {
                hatalar.Add("Havuz / ortak dersler için Çarşamba 10:00-11:00 zaman dilimi bulunamadı.");
                return false;
            }

            foreach (DersBilgi ders in havuzDersleri)
            {
                bool yerlesti = false;

                foreach (ZamanBilgi zaman in carsambaOnOnBirSlotlari)
                {
                    int zamanDakika = Convert.ToInt32((zaman.BitisSaat - zaman.BaslangicSaat).TotalMinutes);

                    if (zamanDakika < ders.SinavSuresi)
                        continue;

                    if (AyniSinifAyniZamandaVarMi(atamalar, ders, zaman))
                        continue;

                    DerslikBilgi derslik = UygunDerslikBul(atamalar, ders, zaman, derslikler);

                    if (derslik == null)
                        continue;

                    AdayAtama aday = new AdayAtama();
                    aday.Ders = ders;
                    aday.Zaman = zaman;
                    aday.Derslik = derslik;
                    aday.Puan = 0;

                    SinavAtama atama = AtamaOlustur(aday, programVersiyon);
                    atamalar.Add(atama);

                    yerlesti = true;
                    break;
                }

                if (!yerlesti)
                {
                    hatalar.Add("Havuz / ortak ders Çarşamba 10:00-11:00 saatine yerleştirilemedi: " + ders.DersAdi);
                    return false;
                }
            }

            return true;
        }

        private List<ZamanBilgi> ArdArdaSlotlariBul(List<ZamanBilgi> gunSlotlari, int gerekliSlotSayisi)
        {
            if (gunSlotlari.Count < gerekliSlotSayisi)
                return null;

            for (int i = 0; i <= gunSlotlari.Count - gerekliSlotSayisi; i++)
            {
                List<ZamanBilgi> adayGrup = new List<ZamanBilgi>();
                adayGrup.Add(gunSlotlari[i]);

                for (int j = i + 1; j < gunSlotlari.Count && adayGrup.Count < gerekliSlotSayisi; j++)
                {
                    ZamanBilgi onceki = adayGrup[adayGrup.Count - 1];
                    ZamanBilgi simdiki = gunSlotlari[j];

                    if (onceki.BitisSaat == simdiki.BaslangicSaat)
                    {
                        adayGrup.Add(simdiki);
                    }
                    else
                    {
                        break;
                    }
                }

                if (adayGrup.Count == gerekliSlotSayisi)
                    return adayGrup;
            }

            return null;
        }

        private DerslikBilgi UygunDerslikBul(
            List<SinavAtama> atamalar,
            DersBilgi ders,
            ZamanBilgi zaman,
            List<DerslikBilgi> derslikler)
        {
            IEnumerable<DerslikBilgi> adayDerslikler = derslikler
                .Where(x => x.Kapasite >= ders.OgrenciSayisi)
                .Where(x => !DerslikAyniZamandaDoluMu(atamalar, x, zaman));

            if (LaboratuvarDersiMi(ders))
            {
                adayDerslikler = adayDerslikler
                    .OrderByDescending(x => LaboratuvarDersligiMi(x))
                    .ThenBy(x => x.Kapasite - ders.OgrenciSayisi);
            }
            else
            {
                adayDerslikler = adayDerslikler
                    .OrderBy(x => LaboratuvarDersligiMi(x))
                    .ThenBy(x => x.Kapasite - ders.OgrenciSayisi);
            }

            return adayDerslikler.FirstOrDefault();
        }

        private SinavAtama AtamaOlustur(AdayAtama secilenAday, int programVersiyon)
        {
            SinavAtama atama = new SinavAtama();

            atama.ProgramVersiyon = programVersiyon;

            atama.DersID = secilenAday.Ders.DersID;
            atama.DersAdi = secilenAday.Ders.DersAdi;
            atama.SinifSeviyeID = secilenAday.Ders.SinifSeviyeID;
            atama.SeviyeNo = secilenAday.Ders.SeviyeNo;
            atama.Kredi = secilenAday.Ders.Kredi;
            atama.OgrenciSayisi = secilenAday.Ders.OgrenciSayisi;
            atama.TipAd = secilenAday.Ders.TipAd;

            atama.ZamanID = secilenAday.Zaman.ZamanID;
            atama.Tarih = secilenAday.Zaman.Tarih;
            atama.BaslangicSaat = secilenAday.Zaman.BaslangicSaat;
            atama.BitisSaat = secilenAday.Zaman.BitisSaat;

            atama.DerslikID = secilenAday.Derslik.DerslikID;
            atama.DerslikAd = secilenAday.Derslik.DerslikAd;
            atama.Kapasite = secilenAday.Derslik.Kapasite;

            return atama;
        }

        private List<DersBilgi> DersleriSirala(List<DersBilgi> dersler, int programVersiyon)
        {
            if (programVersiyon == 1)
            {
                return dersler
                    .OrderBy(x => DersTipiOncelikPuani(x.TipAd))
                    .ThenByDescending(x => x.Kredi)
                    .ThenByDescending(x => x.OgrenciSayisi)
                    .ToList();
            }

            if (programVersiyon == 2)
            {
                return dersler
                    .OrderBy(x => DersTipiOncelikPuani(x.TipAd))
                    .ThenByDescending(x => x.OgrenciSayisi)
                    .ThenByDescending(x => x.Kredi)
                    .ToList();
            }

            return dersler
                .OrderBy(x => DersTipiOncelikPuani(x.TipAd))
                .ThenBy(x => x.SeviyeNo)
                .ThenByDescending(x => x.Kredi)
                .ThenBy(x => Guid.NewGuid())
                .ToList();
        }

        private List<ZamanBilgi> ZamanlariSirala(List<ZamanBilgi> zamanlar, int programVersiyon)
        {
            if (programVersiyon == 1)
            {
                return zamanlar
                    .OrderBy(x => x.Tarih)
                    .ThenBy(x => x.BaslangicSaat)
                    .ToList();
            }

            if (programVersiyon == 2)
            {
                return zamanlar
                    .OrderBy(x => x.Tarih)
                    .ThenBy(x => x.BaslangicSaat)
                    .ToList();
            }

            return zamanlar
                .OrderBy(x => Guid.NewGuid())
                .ToList();
        }

        private List<DerslikBilgi> DerslikleriSirala(List<DerslikBilgi> derslikler, int programVersiyon)
        {
            if (programVersiyon == 2)
            {
                return derslikler
                    .OrderBy(x => x.Kapasite)
                    .ToList();
            }

            return derslikler
                .OrderByDescending(x => x.Kapasite)
                .ToList();
        }

        private int DersTipiOncelikPuani(string tipAd)
        {
            if (string.IsNullOrWhiteSpace(tipAd))
                return 99;

            string tip = tipAd.ToLower();

            if (tip.Contains("rekt"))
                return 1;

            if (tip.Contains("havuz"))
                return 2;

            if (tip.Contains("ortak"))
                return 2;

            if (tip.Contains("zorunlu"))
                return 3;

            if (tip.Contains("lab"))
                return 4;

            if (tip.Contains("laboratuvar"))
                return 4;

            if (tip.Contains("seçmeli") || tip.Contains("secmeli"))
                return 5;

            return 6;
        }

        private int RektorlukDersSirasi(string dersAdi)
        {
            if (string.IsNullOrWhiteSpace(dersAdi))
                return 99;

            string ad = dersAdi.ToLower();

            if (ad.Contains("ingilizce"))
                return 1;

            if (ad.Contains("atatürk") || ad.Contains("ataturk"))
                return 2;

            if (ad.Contains("türk dili") || ad.Contains("turk dili"))
                return 3;

            return 4;
        }

        private bool RektorlukDersiMi(DersBilgi ders)
        {
            if (ders == null || string.IsNullOrWhiteSpace(ders.TipAd))
                return false;

            return ders.TipAd.ToLower().Contains("rekt");
        }

        private bool RektorlukTipiMi(string tipAd)
        {
            if (string.IsNullOrWhiteSpace(tipAd))
                return false;

            return tipAd.ToLower().Contains("rekt");
        }

        private bool HavuzDersiMi(DersBilgi ders)
        {
            if (ders == null || string.IsNullOrWhiteSpace(ders.TipAd))
                return false;

            string tip = ders.TipAd.ToLower();

            return tip.Contains("havuz") || tip.Contains("ortak");
        }

        private bool OrtakDersMi(DersBilgi ders)
        {
            return HavuzDersiMi(ders);
        }

        private bool LaboratuvarDersiMi(DersBilgi ders)
        {
            if (ders == null || string.IsNullOrWhiteSpace(ders.TipAd))
                return false;

            string tip = ders.TipAd.ToLower();

            return tip.Contains("lab") || tip.Contains("laboratuvar");
        }

        private bool LaboratuvarDersligiMi(DerslikBilgi derslik)
        {
            if (derslik == null || string.IsNullOrWhiteSpace(derslik.DerslikAd))
                return false;

            string ad = derslik.DerslikAd.ToLower();

            return ad.Contains("lab") || ad.Contains("laboratuvar");
        }

        private bool AyniSinifAyniZamandaVarMi(List<SinavAtama> atamalar, DersBilgi ders, ZamanBilgi zaman)
        {
            return atamalar.Any(x =>
                x.SinifSeviyeID == ders.SinifSeviyeID &&
                x.ZamanID == zaman.ZamanID);
        }

        private bool DerslikAyniZamandaDoluMu(List<SinavAtama> atamalar, DerslikBilgi derslik, ZamanBilgi zaman)
        {
            return atamalar.Any(x =>
                x.DerslikID == derslik.DerslikID &&
                x.ZamanID == zaman.ZamanID);
        }

        private bool BirinciSinifRektorlukGunuEngeliVarMi(List<SinavAtama> atamalar, DersBilgi ders, ZamanBilgi zaman)
        {
            if (ders.SeviyeNo != 1)
                return false;

            if (RektorlukDersiMi(ders))
                return false;

            bool oGundeBirinciSinifRektorlukVar = atamalar.Any(x =>
                x.SeviyeNo == 1 &&
                RektorlukTipiMi(x.TipAd) &&
                x.Tarih.Date == zaman.Tarih.Date);

            return oGundeBirinciSinifRektorlukVar;
        }

        private int AtamaPuaniHesapla(
            List<SinavAtama> atamalar,
            DersBilgi ders,
            ZamanBilgi zaman,
            DerslikBilgi derslik,
            List<ZamanBilgi> tumZamanlar,
            int programVersiyon,
            Random random)
        {
            int puan = 0;

            int ayniSinifAyniGunSinavSayisi = atamalar.Count(x =>
                x.SinifSeviyeID == ders.SinifSeviyeID &&
                x.Tarih.Date == zaman.Tarih.Date);

            int ayniGunToplamSinavSayisi = atamalar.Count(x =>
                x.Tarih.Date == zaman.Tarih.Date);

            int yuksekKrediAyniGunSayisi = atamalar.Count(x =>
                x.SinifSeviyeID == ders.SinifSeviyeID &&
                x.Tarih.Date == zaman.Tarih.Date &&
                (x.Kredi >= 5 || ders.Kredi >= 5));

            int kapasiteFarki = derslik.Kapasite - ders.OgrenciSayisi;

            bool haftaSonu = zaman.Tarih.DayOfWeek == DayOfWeek.Saturday;
            int tarihSirasi = TarihSirasiBul(tumZamanlar, zaman.Tarih);
            int toplamGunSayisi = tumZamanlar.Select(x => x.Tarih.Date).Distinct().Count();

            if (programVersiyon == 1)
            {
                // Dengeli dağılım
                puan += ayniSinifAyniGunSinavSayisi * 120;
                puan += yuksekKrediAyniGunSayisi * 150;
                puan += ayniGunToplamSinavSayisi * 15;

                if (haftaSonu)
                    puan += 80;

                puan += kapasiteFarki;

                if (LaboratuvarDersiMi(ders) && !LaboratuvarDersligiMi(derslik))
                    puan += 100;

                if (!LaboratuvarDersiMi(ders) && LaboratuvarDersligiMi(derslik))
                    puan += 40;
            }
            else if (programVersiyon == 2)
            {
                // Erken biten
                puan += tarihSirasi * 45;
                puan += ayniSinifAyniGunSinavSayisi * 80;
                puan += yuksekKrediAyniGunSayisi * 90;
                puan += ayniGunToplamSinavSayisi * 5;

                if (haftaSonu)
                    puan += 30;

                puan += kapasiteFarki * 2;

                if (LaboratuvarDersiMi(ders) && !LaboratuvarDersligiMi(derslik))
                    puan += 100;
            }
            else
            {
                // Geniş yayılım
                int hedefGun;

                if (ders.SeviyeNo == 1)
                    hedefGun = 1;
                else if (ders.SeviyeNo == 2)
                    hedefGun = toplamGunSayisi / 3;
                else if (ders.SeviyeNo == 3)
                    hedefGun = (toplamGunSayisi * 2) / 3;
                else
                    hedefGun = toplamGunSayisi - 2;

                puan += Math.Abs(tarihSirasi - hedefGun) * 35;
                puan += ayniSinifAyniGunSinavSayisi * 150;
                puan += yuksekKrediAyniGunSayisi * 170;
                puan += ayniGunToplamSinavSayisi * 20;

                if (haftaSonu)
                    puan += 50;

                puan += kapasiteFarki;

                if (LaboratuvarDersiMi(ders) && !LaboratuvarDersligiMi(derslik))
                    puan += 100;

                puan += random.Next(0, 25);
            }

            if (OrtakDersMi(ders))
                puan -= 10;

            if (ders.OgrenciSayisi > ders.SinifMevcudu)
                puan += ayniSinifAyniGunSinavSayisi * 50;

            return puan;
        }

        private int TarihSirasiBul(List<ZamanBilgi> zamanlar, DateTime tarih)
        {
            List<DateTime> tarihler = zamanlar
                .Select(x => x.Tarih.Date)
                .Distinct()
                .OrderBy(x => x)
                .ToList();

            for (int i = 0; i < tarihler.Count; i++)
            {
                if (tarihler[i].Date == tarih.Date)
                    return i;
            }

            return 0;
        }

        private void ProgramlariKaydet(int takvimID, List<SinavAtama> tumAtamalar)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlTransaction transaction = conn.BeginTransaction();

                try
                {
                    string deleteQuery = @"
                        DELETE FROM dbo.Sinav
                        WHERE DersID IN (
                            SELECT DersID
                            FROM dbo.Ders
                            WHERE BolumID = @BolumID
                        )
                        AND ZamanID IN (
                            SELECT ZamanID
                            FROM dbo.ZamanDilimi
                            WHERE TakvimID = @TakvimID
                        )";

                    using (SqlCommand deleteCmd = new SqlCommand(deleteQuery, conn, transaction))
                    {
                        deleteCmd.Parameters.Add("@BolumID", SqlDbType.Int).Value = bolumID;
                        deleteCmd.Parameters.Add("@TakvimID", SqlDbType.Int).Value = takvimID;
                        deleteCmd.ExecuteNonQuery();
                    }

                    string insertQuery = @"
                        INSERT INTO dbo.Sinav
                        (
                            DersID,
                            ZamanID,
                            DerslikID,
                            ProgramVersiyon
                        )
                        VALUES
                        (
                            @DersID,
                            @ZamanID,
                            @DerslikID,
                            @ProgramVersiyon
                        )";

                    foreach (SinavAtama atama in tumAtamalar)
                    {
                        using (SqlCommand insertCmd = new SqlCommand(insertQuery, conn, transaction))
                        {
                            insertCmd.Parameters.Add("@DersID", SqlDbType.Int).Value = atama.DersID;
                            insertCmd.Parameters.Add("@ZamanID", SqlDbType.Int).Value = atama.ZamanID;
                            insertCmd.Parameters.Add("@DerslikID", SqlDbType.Int).Value = atama.DerslikID;
                            insertCmd.Parameters.Add("@ProgramVersiyon", SqlDbType.Int).Value = atama.ProgramVersiyon;

                            insertCmd.ExecuteNonQuery();
                        }
                    }

                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        private TimeSpan DegeriTimeSpanYap(object deger)
        {
            if (deger is TimeSpan)
                return (TimeSpan)deger;

            if (deger is DateTime)
                return ((DateTime)deger).TimeOfDay;

            return TimeSpan.Parse(deger.ToString());
        }

        public class MotorSonuc
        {
            public bool Basarili { get; set; }
            public string Mesaj { get; set; }
        }

        private class DersBilgi
        {
            public int DersID { get; set; }
            public string DersAdi { get; set; }
            public int BolumID { get; set; }
            public int DersTipiID { get; set; }
            public int SinifSeviyeID { get; set; }
            public int SeviyeNo { get; set; }
            public int SinifMevcudu { get; set; }
            public int Kredi { get; set; }
            public int SinavSuresi { get; set; }
            public int OgrenciSayisi { get; set; }
            public string TipAd { get; set; }
        }

        private class ZamanBilgi
        {
            public int ZamanID { get; set; }
            public int TakvimID { get; set; }
            public DateTime Tarih { get; set; }
            public TimeSpan BaslangicSaat { get; set; }
            public TimeSpan BitisSaat { get; set; }
        }

        private class DerslikBilgi
        {
            public int DerslikID { get; set; }
            public string DerslikAd { get; set; }
            public int Kapasite { get; set; }
        }

        private class SinavAtama
        {
            public int ProgramVersiyon { get; set; }

            public int DersID { get; set; }
            public string DersAdi { get; set; }
            public int SinifSeviyeID { get; set; }
            public int SeviyeNo { get; set; }
            public int Kredi { get; set; }
            public int OgrenciSayisi { get; set; }
            public string TipAd { get; set; }

            public int ZamanID { get; set; }
            public DateTime Tarih { get; set; }
            public TimeSpan BaslangicSaat { get; set; }
            public TimeSpan BitisSaat { get; set; }

            public int DerslikID { get; set; }
            public string DerslikAd { get; set; }
            public int Kapasite { get; set; }
        }

        private class AdayAtama
        {
            public DersBilgi Ders { get; set; }
            public ZamanBilgi Zaman { get; set; }
            public DerslikBilgi Derslik { get; set; }
            public int Puan { get; set; }
        }
    }
}