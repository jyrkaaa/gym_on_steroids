# Gym Tracker WebApp



## Backend

The projects brain is written in c# with .Net and uses clean architecture to create a reliable and scalable backend for the frontend. Main Focus is placed on the transversal between layers, from the database layer to the api layer. Everything is mapped wihtout auto-mappers, and authorization is with JWT.

## Frontend
The Main frontend is with Vue and TS, with a little bit of React for fun. The frontend fetches data from the backend with restful API and most of the logic is in the backend, in the BLL or repository layer. All of the authethication is tweakable from the backend and every user-specific data is guarded at the lowest layer of the backend. This prevents front-end manipulation and injection attacks.


## License
For open source projects, say how it is licensed.

## Project status
The project is deployed, but not hosted, it is written with postgres DB, but can be easily changed because of generics.
