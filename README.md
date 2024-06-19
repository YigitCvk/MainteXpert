# MainteXpert

MainteXpert, bakım süreçlerini dijitalleştiren ve optimize eden bir uygulamadır. Bu proje, farklı mikroservisler ve kimlik doğrulama yetkilendirme işlemleri için IdentityServer4 kullanarak oluşturulmuştur.

## Başlangıç

Bu proje, bakım süreçlerini yönetmek için çeşitli mikroservisler içermektedir. API'ler arasındaki iletişim ve yetkilendirme IdentityServer4 ile sağlanmaktadır.

## Gereksinimler

Projenin çalışması için aşağıdaki araçların yüklü olması gerekmektedir:

- .NET 8.0 SDK
- IdentitServer4 (.NET 3.1)
- Docker
- MongoDB
- SQL Server
- Visual Studio veya Visual Studio Code

## İçindekiler

Mikroservisler
Proje, aşağıdaki mikroservislerden oluşmaktadır:

IdentityServer: Kimlik doğrulama ve yetkilendirme hizmeti.
ErrorCardService: Hata kartı yönetimi.
ActivityService: Aktivite yönetimi.
ReportService: Raporlama hizmeti.
UserService: Kullanıcı yönetimi.
LookupService: Kod ve referans verilerinin yönetimi.
GatewayService: API Gateway hizmeti.
