
# Home Screen Controller
<a id="home-screen-controller"></a>

Provides aggregated data suitable for populating a main home screen or dashboard.

---
### Get Top Collections
<a id="get-top-collections"></a>

> `GET /api/v1/home/collections/top`

| Method | Path | Authentication |
| :--- | :--- | :--- |
| `GET` | `/api/v1/home/collections/top` | Optional |

**Description**
This endpoint is designed to populate a "featured" or "top" section on a home screen by retrieving a list of top-rated or most popular collections.

**Responses**
| Status Code | Reason | Response Body Content |
| :--- | :--- | :--- |
| `200 OK` | Success | An array of `SendCollectionDTO_Thumb` objects. |

#### Response Structure
**`200 OK` Response Body** (Array of `SendCollectionDTO_Thumb`)
```json
[
  {
    "collectionName": "string",
    "description": "string",
    "isPublic": true,
    "collectionID": 0,
    "addedTime": "2024-05-21T12:00:00Z",
    "categories": [
      {
        "categoryName": "string"
      }
    ]
  }
]
```

#### Example Response
**`200 OK` Response Body**
```json
[
  {
    "collectionName": "Introduction to Astronomy",
    "description": "A collection of basic questions about our solar system and beyond.",
    "isPublic": true,
    "collectionID": 101,
    "addedTime": "2024-05-21T13:00:00Z",
    "categories": [
      {
        "categoryName": "Science"
      }
    ]
  },
  {
    "collectionName": "World War II History",
    "description": "Key events and figures from WWII.",
    "isPublic": true,
    "collectionID": 102,
    "addedTime": "2024-05-20T10:00:00Z",
    "categories": [
      {
        "categoryName": "History"
      }
    ]
  }
]
```
---
### Get Top Followers
<a id="get-top-followers"></a>

> `GET /api/v1/home/followers/top`

| Method | Path | Authentication |
| :--- | :--- | :--- |
| `GET` | `/api/v1/home/followers/top` | Optional |

**Description**
This endpoint is designed to populate a "leaderboard" or "top creators" section on a home screen by retrieving a list of top users (e.g., by points or number of followers).

**Responses**
| Status Code | Reason | Response Body Content |
| :--- | :--- | :--- |
| `200 OK` | Success | An array of `SendUserDTO` objects. |

#### Response Structure
**`200 OK` Response Body** (Array of `SendUserDTO`)
```json
[
  {
    "userId": 0,
    "username": "string",
    "person": {
      "firstName": "string",
      "secondName": "string",
      "lastName": "string",
      "address": "string",
      "gender": true,
      "countryID": 0,
      "dateOfBirth": "2024-05-21T12:00:00Z",
      "email": "string",
      "notes": "string",
      "preferredLanguageID": 0,
      "personID": 0,
      "joinedDate": "2024-05-21T12:00:00Z"
    }
  }
]
```

#### Example Response
**`200 OK` Response Body**
```json
[
  {
    "userId": 123,
    "username": "newuser",
    "person": {
      "firstName": "Jane",
      "secondName": "M.",
      "lastName": "Doe",
      "address": "789 Pine Street, New City",
      "gender": false,
      "countryID": 2,
      "dateOfBirth": "1995-08-22T00:00:00Z",
      "email": "jane.doe@example.com",
      "notes": "Updated my notes.",
      "preferredLanguageID": 1,
      "personID": 45,
      "joinedDate": "2024-05-21T12:30:00Z"
    }
  },
  {
    "userId": 125,
    "username": "topcreator",
    "person": {
      "firstName": "Alex",
      "secondName": null,
      "lastName": "Ray",
      "address": null,
      "gender": true,
      "countryID": 1,
      "dateOfBirth": "1990-01-15T00:00:00Z",
      "email": "alex.ray@example.com",
      "notes": null,
      "preferredLanguageID": 1,
      "personID": 47,
      "joinedDate": "2023-01-10T09:00:00Z"
    }
  }
]
```
---
---
