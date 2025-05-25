1️⃣ Kullanıcı Yönetimi
✅ Kullanıcı kaydı (register)
✅ Kullanıcı girişi (login, JWT token)
✅ Kullanıcı bilgilerini görme, güncelleme, silme + sadece kullanıcı kendi infosunu görür
❌ Parola sıfırlama (opsiyonel)
✅ Rollere göre yetkilendirme (admin / normal kullanıcı)

2️⃣ Harcama Yönetimi (Transactions)
✅ Harcama ekleme, düzenleme, silme, listeleme
✅ Kategorilere göre harcamaları sınıflandırma (yemek, ulaşım, kira vs.)
❌ Harcama raporları ve grafikler
❌ Harcama analizi (AI destekli öneriler, örn. tasarruf noktaları)

3️⃣ Para Transferleri (Transfers)
✅ Kullanıcı hesapları arası transfer - tek hesap var şuanlk
✅ Diğer kullanıcıya transfer
✅ Transferler isFast mi değil mi kontrolü var bu sayede ya gece 12.00 da sistem tarafından ya da belirli komisyon karşılığında anlık geçiriliyor.
✅ Transfer geçmişi ve detayları

4️⃣ Bankalarla Entegrasyon (Mock API)
✅ Kredi kartı borcu çekme
✅ Kredi borcu çekme
❌ Banka bakiyesi sorgulama - mock api yazılcak
❌ Kredi teklifleri karşılaştırma
❌ Mevduat/yatırım ürünleri karşılaştırma
❌ Kredi skoru analizi ve öneriler

5️⃣ Yatırım ve Finansal Piyasa Verileri
❌ Altın, gümüş, platin fiyatları - mock api yazılcak
❌ BIST100, NASDAQ, DAX endeks verileri
❌ Kullanıcı portföy takibi
❌ Yatırım önerileri (risk profiline uygun)

6️⃣ Otomatik Ödeme Talimatları
✅ Kira, fatura, telefon, internet vb. için otomatik ödeme talimatı oluşturma
✅ Talimat listeleme, düzenleme, silme
✅ Hangfire job’ları ile belirli zamanda ödemeleri gerçekleştirme

7️⃣ Abonelik ve Fatura Takibi
✅ Kullanıcının tüm aboneliklerini (Netflix, Spotify, vs.) listeleme
❌ Fatura takip ekranı (elektrik, su, doğalgaz, internet vs.)
❌ Yaklaşan ödeme tarihleri için bildirim/hatırlatma
❌ Gereksiz abonelikleri tespit ve iptal önerisi

8️⃣ Hedef Bazlı Bütçeleme ve Tasarruf Planları
✅ Kullanıcının belirlediği hedefler (tatil, araba, eğitim) için bütçe planı oluşturma
✅ Aylık/haftalık tasarruf planı hazırlama
❌ Otomatik tasarruf kuralları (örn. round-up: küsuratları birikim hesabına atma)
❌ Hedef ilerleme takibi ve uyarılar

9️⃣ AI Destekli Finansal Öneriler
❌ Harcama alışkanlıklarına göre kişisel öneriler
❌ Bütçe aşımı uyarıları
❌ Kredi ve yatırım simülasyonları (hangi hareketin kredi skorunu nasıl etkiler)

🔄 Arka Plan ve Teknik Operasyonlar (Hangfire ile)
✅ Borç ve yatırım verilerini düzenli güncelleme (örn. gece job’u)
✅ Otomatik ödeme ve transfer job’ları
✅ Bildirim ve hatırlatma job’ları
✅ Veritabanı yedekleme/senkronizasyon job’ları

