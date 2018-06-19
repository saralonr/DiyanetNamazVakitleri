# DiyanetNamazVakitleri
Diyanet İşleri'nin resmi web sitesinden ilçeye göre namaz vakitlerini alan ve servis eden WebAPI 

## Endpoint Listesi ve İşlevleri :

### GET Method - *GetPrayerTimes()*
**- PARAMETRELER** 
  * **stateID, int:** *GetDistricts()*'den dönen Districts listesinden bir ID verilmelidir.

**- RESPONSE** 
```json
{
"Date": "19 Haziran Salı 2018",
"Imsak": "03:24",
"Gunes": "05:25",
"Ogle": "13:11",
"Ikindi": "17:10",
"Aksam": "20:46",
"Yatsi": "22:37"
}
``` 
### GET Method - *GetCountries()*

**- RESPONSE** 
```json
[
   {
   "ID": 33,
   "Country": "ABD"
   },
   {
   "ID": 166,
   "Country": "AFGANISTAN"
   }
]
``` 
### GET Method - *GetCities()*
**- PARAMETRELER** 
  * **countryID, int:** *GetCountries()*'den dönen Countries listesinden bir ID verilmelidir.

**- RESPONSE** 
```json
[
   {
   "ID": 500,
   "CountryID": 2,
   "CityName": "ADANA"
   },
   {
   "ID": 501,
   "CountryID": 2,
   "CityName": "ADIYAMAN"
   }
]
``` 
### GET Method - *GetDistricts()*
**- PARAMETRELER** 
  * **countryID, int:** *GetCountries()*'den dönen Countries listesinden bir ID verilmelidir.
  * **cityID, int:** *GetCities()*'den dönen Cities listesinden bir ID verilmelidir.
  
**- RESPONSE** 
```json
[
   {
   "ID": 9535,
   "CountryID": 2,
   "CityID": 539,
   "DistrictName": "ARNAVUTKOY"
   },
   {
   "ID": 17865,
   "CountryID": 2,
   "CityID": 539,
   "DistrictName": "AVCILAR"
   }
]
``` 

### NOTLAR
**Hatalı İstekler**

```
* Hatalı isteklerde, parametre eksikse geriye 0 cevabı döndürülür.
* Parametre hatalıysa ya da istek sırasında sunucuda hata meydana gelirse geriye -1 cevabı döndürülür.
``` 
**CORS**
```
* CORS etkinleştirilmiştir. Javascript ile istek gönderilebilir.
``` 
_Jun 19, 2018._
