
# Movie Recommendation App

Proje içerisinde aşağıdaki teknolojiler/kütüphaneler kullanılmıştır.
```
.NET 7
EfCore
Sql Server
Redis
RabbitMQ
JWT
Swagger
Hangfire
PagedList
ReDoc
```

Proje çalıştığında otomatik olarak 1000 adet film otomatik DB'ye kayıt edilip cache ediliyor.

60 dakikada bir cache ile karşılaştırma yapılıp update yapılıyor.

MovieApp.postman_collection.json dosyası ile Postman'a import yapabilirsiniz.



## API Reference

### Register

Kayıt olmak için bu methodu kullanmalısınız.
```
 POST /api/Auth/register
```

| Body | Type     | Description                |
| :-------- | :------- | :------------------------- |
| `email` | `string` |  Kayıt yapılacak email adresi. |
| `password` | `string` | Kayıt yapılacak şifre. |

### Login
Giriş yapmak için bu methodu kullanmalısınız, diğer tüm methodları kullanmak için JWT token ihtiyacınız var, login olduktan sonra token değerini göreceksiniz. Token 60 dakika geçerlidir.
```
 POST /api/Auth/login
```

| Body | Type     | Description                |
| :-------- | :------- | :------------------------- |
| `email` | `string` | Giriş yapılacak email adresi. |
| `password` | `string` | Giriş yapılacak şifre. |

### Movie
```
 GET /api/Movie
```
Sayfa sayısı ve sayfa boyutunu parametre olarak alır, buna göre tablo döndürür.
| Parameter | Type     | Description                |
| :-------- | :------- | :------------------------- |
| `pageNumber` | `int` |  Db'den okunacak filmin ID değeri. |
| `pageSize` | `int` |  Db'den okunacak filmin ID değeri. |

| Header | Type     | Description                |
| :-------- | :------- | :------------------------- |
| `Authorization` | `string` | Bearer {JWT Auth Token} |

```
 GET /api/Movie/{id}
```
Verilen id değerine göre filmi ve ona verilen notları döndürür.
| Parameter | Type     | Description                |
| :-------- | :------- | :------------------------- |
| `id` | `int` |  Db'den okunacak filmin ID değeri. |

| Header | Type     | Description                |
| :-------- | :------- | :------------------------- |
| `Authorization` | `string` | Bearer {JWT Auth Token} |


```
 POST /api/Movie
```
Film eklemek için kullanılabilir.
| Body | Type     | Description                |
| :-------- | :------- | :------------------------- |
| `id` | `int` |  Filmin ID değeri. |
| `title` | `string` |  Filmin başlığı. |
| `voteAverage` | `double` |  Filme verilen ortalama puan. |
| `voteCount` | `int` |  Filme verilen oyların sayısı. |

| Header | Type     | Description                |
| :-------- | :------- | :------------------------- |
| `Authorization` | `string` |  Bearer {JWT Auth Token} |


```
 DELETE /api/movie/addMovieNotes
```
Film silmek için kullanılabilir.
| Parameter | Type     | Description                |
| :-------- | :------- | :------------------------- |
| `movieId` | `int` | Silinecek filmin ID değeri. |

| Header | Type     | Description                |
| :-------- | :------- | :------------------------- |
| `Authorization` | `string` | Bearer {JWT Auth Token} |


```
 PATCH /api/Movie
```
Film düzenlemek için kullanılabilir.
| Body | Type     | Description                |
| :-------- | :------- | :------------------------- |
| `id` | `int` |  Filmin ID değeri. |
| `title` | `string` |  Filmin başlığı. |
| `voteAverage` | `double` |  Filme verilen ortalama puan. |
| `voteCount` | `int` |  Filme verilen oyların sayısı. |

| Header | Type     | Description                |
| :-------- | :------- | :------------------------- |
| `Authorization` | `string` |  Bearer {JWT Auth Token} |

###Recommend

```
 POST /api/Recommend/recommend-movie
```
Alıcıya parametler ile gönderilen filmi mail olarak gönderir.
| Body | Type     | Description                |
| :-------- | :------- | :------------------------- |
| `senderUsername` | `string` |  Gönderici kullanıcı adı. |
| `receiverMail` | `string` |  Alıcı mail adresi. |
| `movieName` | `string` |  Filmin adı. |

| Header | Type     | Description                |
| :-------- | :------- | :------------------------- |
| `Authorization` | `string` |  Bearer {JWT Auth Token} |

###Vote

```
 POST /api/Vote
```
Parametreden alınan id değerine göre filme not verir.
| Body | Type     | Description                |
| :-------- | :------- | :------------------------- |
| `movieId` | `int` |  Filmin ID değeri. |
| `comment` | `string` |  Film için verilen yorum. |
| `point` | `int` |  Filme verilen puan. 1-10 |

| Header | Type     | Description                |
| :-------- | :------- | :------------------------- |
| `Authorization` | `string` |  Bearer {JWT Auth Token} |

## Appsettings.json

### ConnectionStrings

| Parameter | Type     | Description                |
| :-------- | :------- | :------------------------- |
| `DefaultConnection` | `string` | **Required**. SQL Server connection string. |

#### Jwt

| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `Secret`      | `string` | **Required**. JWT Secret key. |
| `Issuer`      | `string` | **Required**. JWT Issuer |
| `Audience`      | `string` | **Required**. JWT Audience |

#### RedisSettings

| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `CacheTime`      | `int` | **Required**. Redis üzerindeki tutulacak cache süresini tanımlar. |
| `RedisConnectionString`      | `string` | **Required**. Redis urlsi. |

#### RabbitMQ

| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `ConnectionString`      | `int` | **Required**. RabbitMQ Cloud için bağlantı URL'si. |

#### TheMovieDB

| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `ApiKey`      | `string` | **Required**. TheMovieDB API KEY |
| `ApiUrl`      | `string` | **Required**. TheMovieDB Base API URL |
