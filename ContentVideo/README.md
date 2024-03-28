
---

# Documentation de l'API ContentVideo

Bienvenue dans la documentation de l'API ContentVideo. Cette API permet de gérer des utilisateurs, des rôles, des tags, et des vidéos dans une application de gestion de contenu vidéo.

## Commencer

Pour commencer à utiliser l'API ContentVideo, assurez-vous d'avoir les prérequis suivants :

- .NET 8 SDK installé sur votre machine.
- Un client HTTP comme Postman ou cURL pour tester les requêtes.

## Configuration

Avant de lancer l'application, configurez la chaîne de connexion à la base de données et la clé secrète JWT dans le fichier `appsettings.json` :

```json
{
  "ConnectionStrings": {
    "ContentVideoConnectionString": "VotreChaîneDeConnexion"
  },
  "JwtKey": "VotreCléSecrèteJWT"
}
```

## Lancer l'Application

Exécutez l'application en utilisant la commande suivante dans le répertoire racine du projet :

```sh
dotnet run
```

L'API sera accessible via `http://localhost:5000` par défaut.

## Authentification

### Connexion

Pour obtenir un token JWT, envoyez une requête POST à `/api/User/login` avec un corps de requête contenant un nom d'utilisateur et un mot de passe :

```json
{
  "Username": "votreNomUtilisateur",
  "Password": "votreMotDePasse"
}
```

### Utilisation du Token JWT

Incluez le token JWT obtenu dans l'en-tête `Authorization` de vos requêtes pour accéder aux endpoints sécurisés :

```http
Authorization: Bearer votreTokenJWT
```

## Endpoints

### Users

- **POST /api/User/login** : Connexion utilisateur.
- **POST /api/User** : Créer un nouvel utilisateur.
- **GET /api/User** : Récupérer tous les utilisateurs.
- ...

### Roles

- **POST /api/Role** : Créer un nouveau rôle.
- **GET /api/Role** : Récupérer tous les rôles.
- ...

### Tags

- **POST /api/Tag** : Créer un nouveau tag.
- **GET /api/Tag** : Récupérer tous les tags.
- ...

### Videos

- **POST /api/Video** : Ajouter une nouvelle vidéo.
- **GET /api/Video** : Récupérer toutes les vidéos.
- ...

## Swagger UI

Pour une documentation interactive de l'API et pour tester les endpoints, naviguez vers `/swagger` après avoir lancé l'application.
