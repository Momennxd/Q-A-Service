
# Categories Controller
<a id="categories-controller"></a>

Manages the creation and retrieval of categories used to classify content.

---
### Create a Category
<a id="create-a-category"></a>

> `POST /api/v1/categories`

| Method | Path | Authentication |
| :--- | :--- | :--- |
| `POST` | `/api/v1/categories` | **Required** |

**Description**
Creates a new category that can be used to tag questions and collections. Category names are typically unique.

#### Request Structure
**Request Body** (`CreateCategoryDTO`)
```json
{
  "categoryName": "string"
}
```

#### Example Request
**Request Body**
```json
{
  "categoryName": "Science"
}
```

**Responses**
| Status Code | Reason | Response Body Content |
| :--- | :--- | :--- |
| `200 OK` | Success | A `SendCategoryDTO` object for the new category. |
| `400 Bad Request` | Already Exists | A category with that name already exists. |

#### Response Structure
**`200 OK` Response Body** (`SendCategoryDTO`)
```json
{
  "categoryID": 0,
  "categoryName": "string"
}
```

#### Example Response
**`200 OK` Response Body**
```json
{
  "categoryID": 10,
  "categoryName": "Science"
}
```
---
### Get Categories
<a id="get-categories"></a>

> `GET /api/v1/categories/{RowsCount}`

| Method | Path | Authentication |
| :--- | :--- | :--- |
| `GET` | `/api/v1/categories/{RowsCount}` | Optional |

**Description**
Retrieves a list of categories, with optional filtering by name and a limit on the number of results. Useful for populating a category selector or search autocomplete.

#### Request Structure
**Path Parameters**
| Parameter | Type | Required | Description |
| :--- | :--- | :--- | :--- |
| `RowsCount` | integer | Yes | The maximum number of categories to return. |

**Query Parameters**
| Parameter | Type | Required | Description |
| :--- | :--- | :--- | :--- |
| `CategorySumName` | string | No | A search string to filter category names (e.g., "Sci" would match "Science"). |

#### Example Request
**Request URL**
`/api/v1/categories/10?CategorySumName=Sci`

**Responses**
| Status Code | Reason | Response Body Content |
| :--- | :--- | :--- |
| `200 OK` | Success | An array of `SendCategoryDTO` objects. |

#### Response Structure
**`200 OK` Response Body** (Array of `SendCategoryDTO`)
```json
[
  {
    "categoryID": 0,
    "categoryName": "string"
  }
]
```

#### Example Response
**`200 OK` Response Body**
```json
[
  {
    "categoryID": 10,
    "categoryName": "Science"
  }
]
```
---
---
