# 📊 Finansal Uygulama Özellikleri ve Durum Takibi (.NET)

## 1️⃣ Kullanıcı Yönetimi

- [x] Kullanıcı kaydı (register)
- [x] Kullanıcı girişi (login, JWT token)
- [x] Kullanıcı bilgilerini görme, güncelleme, silme  
  _Not: Sadece kullanıcı kendi bilgilerini görebilir_
- [ ] Parola sıfırlama (opsiyonel)
- [x] Rollere göre yetkilendirme (admin / normal kullanıcı)

---

## 2️⃣ Harcama Yönetimi (Transactions)

- [x] Harcama ekleme, düzenleme, silme, listeleme
- [x] Kategorilere göre harcamaları sınıflandırma (yemek, ulaşım, kira vs.)
- [ ] Harcama raporları ve grafikler
- [ ] Harcama analizi (AI destekli öneriler, örn. tasarruf noktaları)

---

## 3️⃣ Para Transferleri (Transfers)

- [x] Kullanıcı hesapları arası transfer (şu an tek hesap var)
- [x] Diğer kullanıcıya transfer
- [x] isFast kontrolü (gece 12.00'de ya da komisyon karşılığı anlık transfer)
- [x] Transfer geçmişi ve detayları

---

## 4️⃣ Bankalarla Entegrasyon (Mock API)

- [x] Kredi kartı borcu çekme
- [x] Kredi borcu çekme
- [ ] Banka bakiyesi sorgulama (mock API yazılacak)
- [ ] Kredi teklifleri karşılaştırma
- [ ] Mevduat/yatırım ürünleri karşılaştırma
- [ ] Kredi skoru analizi ve öneriler

---

## 5️⃣ Yatırım ve Finansal Piyasa Verileri

- [x] Altın, gümüş, platin fiyatları (mock API yazılacak)
- [x] BIST100, NASDAQ, DAX endeks verileri
- [x] Kullanıcı portföy takibi
- [ ] Yatırım önerileri (risk profiline uygun)

---

## 6️⃣ Otomatik Ödeme Talimatları

- [x] Kira, fatura, telefon, internet vb. için otomatik ödeme talimatı oluşturma
- [x] Talimat listeleme, düzenleme, silme
- [x] Hangfire job’ları ile belirli zamanda ödemeleri gerçekleştirme

---

## 7️⃣ Abonelik ve Fatura Takibi

_Abonelik yönetimi ve otomatik ödeme talimatları benzer, sadece alan farkı var_

- [x] Kullanıcının tüm aboneliklerini (Netflix, Spotify, vs.) listeleme
- [x] Fatura takip ekranı (elektrik, su, doğalgaz, internet vs.)
- [x] Yaklaşan ödeme tarihleri için bildirim/hatırlatma
- [x] Gereksiz abonelikleri tespit ve iptal önerisi

---

## 8️⃣ Hedef Bazlı Bütçeleme ve Tasarruf Planları

- [x] Kullanıcının belirlediği hedefler (tatil, araba, eğitim) için bütçe planı oluşturma
- [x] Aylık/haftalık tasarruf planı hazırlama
- [x] Otomatik tasarruf kuralları (örn. round-up: küsuratları birikim hesabına atma)

---

## 9️⃣ AI Destekli Finansal Öneriler

- [ ] Harcama alışkanlıklarına göre kişisel öneriler
- [ ] Bütçe aşımı uyarıları
- [ ] Kredi ve yatırım simülasyonları (hangi hareketin kredi skorunu nasıl etkiler)

---

## 🔄 Arka Plan ve Teknik Operasyonlar (Hangfire ile)

- [x] Borç ve yatırım verilerini düzenli güncelleme (örn. gece job’u)
- [x] Otomatik ödeme ve transfer job’ları
- [x] Bildirim ve hatırlatma job’ları
- [x] Veritabanı yedekleme/senkronizasyon job’ları

---

## 🚧 TODO / Planlanan Büyük Geliştirmeler

- [ ] Tek kullanıcının birden fazla hesabı olabilir.  
  _Not: Şu an mimari tek hesaba göre kodlandı, zaman olunca entegre edilecek._
