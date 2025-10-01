# Tower Defence Prototype (KFA Case Study)

Bu proje, KFA Entertainment tarafından verilen case study kapsamında geliştirilmiştir.

Amaç; Unityde 3D bir ortamda 2D assetleri kullanarak (Billboard tarzı) temel bir oyun mekaniği geliştirme, oyun motoruna hâkimiyet ve kod yapısı oluşturma becerilerini değerlendirmek.

## Kullanılan Oyun Motoru ve Versiyonu
- Unity 6000.0.51f1 LTS

## Proje Özeti
Bu prototipte, Tower Defence türünde temel bir oyun döngüsü oluşturulmuştur:  
- Oyuncu karakteri, **WASD ile hareket eder** ve en yakın düşmana otomatik saldırı yapar.  
- Düşmanlar **dalga dalga (Wave based)** sahneye gelir ve belirlenmiş yolu takip ederek haritanın sonuna ulaşmaya çalışır.  
- Düşmanların istatistikleri (can, hız, saldırı gücü) renk değiştirerek temsil edilir.(Mavi = hız, Kırmızı = can, Yeşil = güç)
- Oyun 3D bir dünya üzerinde, ancak tüm karakterler ve düşmanlar 2D sprite’larla bilboard tekniğiyle görselleştirilmiştir.

## Oynanış Mekanikleri
### Oyuncu
- WASD ile hareket.  
- En yakındaki rakibe otomatik menzilli saldırı.
- Hasar alınca **I-frame** (geçici dokunulmazlık) mekaniği.

### Düşmanlar
- Dalga tabanlı spawn sistemi.
- Önceden tanımlı yolu takip ederler.
- Renk değişimleriyle farklı özellikler (can/hız/güç).
- Bir tuş ile erken wave çağırma(n tuşu).

#### Normal-Düşmanlar
- Goblin:
<img src="https://github.com/muratkrdl/KFA-Case/blob/main/IMAGES/Goblin.png" width="96px">

- Wolf:
<img src="https://github.com/muratkrdl/KFA-Case/blob/main/IMAGES/Wolf.png" width="96px">

- Boss:
<img src="https://github.com/muratkrdl/KFA-Case/blob/main/IMAGES/Boss.png" width="96px">

### Harita
- Yolun başı ve sonu vardır.  
- Haritanın kenarına ulaşıldığında düşman başarılı sayılır(sağ).

### Stres Testi
- 5000 animasyonlu bir stress testi yapılmıştır, testin video linki:

[StressTest](https://www.youtube.com/watch?v=QTU1Qaig17Q)

### Extralar
- Basit VFX ve partikül efektleri.  
- Ses efektleri (vur, vurulma, yürüme, menzilli saldırı).  
- Her 5 wave de bir boss wave.  

## Varsayımlar ve Açıklamalar
- En çok performans isteyen düşmanlar olduğu için düşmanların sistemleri için(hareket sistemi, animasyon gibi...) ECS(Entity Component System) ile birlikte hybrid bir sistem yazılmış ve kullanılmıştır.
- Performansı daha fazla arttırabilmek için düşman sistemini hybrid değil tamamen ECS yapıya dönüştürülebilir.
- Oyundaki tüm görseller ve sesler placeholder/open-source varlıklardan alınmıştır.
- Tüm billboard karakterler SpriteRenderer + SpriteSetter script ile kameraya döndürülmektedir.
- Dalga sistemi için temel bir WaveManager yazılmıştır. Bu sistem istenirse WaveConfig değişkenleri değiştirilerek kolayca genişletilebilir.

## Genel Mekaniklerin Gösterimi
- CoreGameplay videosu linki:

[Gameplay](https://www.youtube.com/watch?v=bg7A8Sjn7iQ)

---

💡 **Not:** Bu proje bir prototiptir. Görsel ve ses kalitesi ikinci plandadır. Odak, temel oyun döngüsünün sağlıklı ve genişletilebilir bir şekilde oluşturulmasıdır.

---

