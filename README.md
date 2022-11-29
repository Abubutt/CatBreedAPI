# CatBreedAPI

## Reason For API
This api allows the user to easily search for different information on cat breeds as well as see what cats are available.

## Changed API Idea
I changed the API from being about populations to cat breeds because I myself have a cat and the hassle to find a cat online made me want to create an api for it.

## Endpoints

### Breeds
    
    Request Body:
      
      {
        "breedId": 0,
        "breedName": "string",
        "priceRange": "string",
        "location": "string",
        "cats": [
          {
            "catId": 0,
            "catName": "string",
            "age": 0,
            "breedId": 0
          }
        ]
      }
      
      
      Response Body:
      
      {
        "statusCode": 0,
        "statusDescription": "string",
        "breeds": [
          {
            "breedId": 0,
            "breedName": "string",
            "priceRange": "string",
            "location": "string",
            "cats": [
              {
                "catId": 0,
                "catName": "string",
                "age": 0,
                "breedId": 0
              }
           ]
        }
      ]
    }
    
    
    
    GET
      - api/Breeds  ==> returns all the breeds stored
      - api/Breeds/{name} ==> returns the specified breed
      
    POST
      - api/Breeds/ ==> user is able to add a breed
    
    DELETE
      - api/Breeds/{name} ==> deletes the specified breed
      
### Cats
    
    Request Body:
      
      {
        "catId": 0,
        "catName": "string",
        "age": 0,
        "breedId": 0
      }
      
      
      Response Body:
      
      {
        "statusCode": 0,
        "statusDescription": "string",
        "cats": [
          {
           "catId": 0,
           "catName": "string",
           "age": 0,
           "breedId": 0
          }
        ]
      }
    
    
    
    GET
      - api/Cats  ==> returns all the cats stored
      - api/Cats/{name} ==> returns the specified cat
      
    POST
      - api/Cats/ ==> user is able to add a cat
    
      
     
      
      
