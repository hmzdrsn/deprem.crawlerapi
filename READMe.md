
# API Kullanımı

Api Doc:
https://app.swaggerhub.com/apis/HMZDRSN64/APIDEPREM/1.0

## Son Depremi Getir

```http
  GET /api/deprem/getLast
```

## Yakınlarda bul

```http
  GET /api/deprem/findBy/{tarih}/{enlem}/{boylam}
```

| Parametre | Tip     | Açıklama                       |
| :-------- | :------- | :-------------------------------- |
| `tarih`      | `DateTime` | **Gerekli**. Depremin tarih değeri |
| `enlem`      | `double` | **Gerekli**. Depremin enlem değeri |
| `boylam`      | `double` | **Gerekli**. Depremin boylam değeri|


