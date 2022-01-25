# CurrencyExchange
Currency Exchange API

### Two Endpoints:
- GET /api/Default/{code}: this endpoint will receive a ISO code to get the current exchange rate from and public API.
```json
{code}
```
- POST /api/Default/: allow to the user to do a purchase with the following structure:
```json
{
  "userID": "string",
  "amount": 0,
  "currencyCode": "string"
}
```
- GET: /api/Default/: Get list of records stored in the database.

> For testing purpose use the userID "001", code used to initialize the database.

![Info](https://github.com/elymichael/CurrencyExchange/blob/master/info.png)
