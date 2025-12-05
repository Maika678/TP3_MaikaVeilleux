# Rapport TP3 de Cybersec Maika Veilleux

## Attaque 1: BD fuitée et mot de passe

1. Déterminer où le .exe stocke la BD locale : <br>
   a) Démarrer l'outil Process Monitor puis lancer l'application et effectuer une action qui effectue une modification (ex. : créer un utilisateur). Une fois l'action terminée, arrêtez la capture en cliquant sur le bouton "Capture". <br>
   ![creer.png](creer.png) <br>
   b) Entrer des filtres pour obtenir une opération d'écriture provenant de notre exécutable et localiser l'emplacement de la BD. Dans notre cas, la BD se situe dans le répertoire C:\Users\1776430\data\data\. <br>
   ![filtres.png](filtres.png) <br>
2. Ouvrir le fichier de la BD trouvé plus haut avec l'application DataGrip : <br>
   ![data.png](data.png) <br>
3. Dans l'application, créer de nouveaux comptes utilisateurs et utiliser l'option "Les premiers ministres". <br>
   ![ministre.png](ministre.png) <br>
4. Rafraichir la table MUtilisateur et constater que nous avons accès aux mots de passes hachés des comptes créés et de tous les premiers ministres.
   ![table.png](table.png) <br>
5. 5 des 7 mots de passes ont pu être craqués à l'aide de CrackStation.
   ![crack.png](crack.png) <br>

### Correctif implanté

Implanter Bcrypt dans le code source de l'application et changer les fonctions "HacherLeMotDePasse" et "VerifierLeMotDePasse" pour utiliser la librairie qui implante Bcrypt. <br>
![code.png](code.png) <br>

Preuve que l'attaque ne fonctionne plus :
1. créer un utilisateur et utiliser l'option "Les premiers ministres". <br> 
   ![util.png](util.png) <br>
2. Rafraichir la table MUtilisateur et tenter de craquer le mot de passe des ministres et du nouvel utilisateur à l'aide de CrackStation. <br>
   ![utilmdp.png](utilmdp.png) <br>
   ![crackimpo.png](crackimpo.png) <br>
Les mots de passes n'ont pas pu être craqués.
   


## Attaque 2: BD fuitée et encryption

1. Dans l'application, créer un utilisateur avec le NAS 123456789. <br>
   ![test.png](test.png) <br>
2. Rafraichir la table MUtilisateur pour construire la table de traduction. Dans notre cas : 123456789 = bdfhjlnpr. Donc, 1=b, 2=d et ainsi de suite. <br>
   ![testbd.png](testbd.png) <br>
3. Utiliser l'IA pour traduire tout les autres NAS chiffrés. <br>
   ![gpt.png](gpt.png) <br>
   ![gptrep.png](gptrep.png) <br>

### Correctif implanté

implanter un algorythme d'encryption dans l'application et modifier les fonctions "Encrypter" et "Decrypter" pour utiliser cet algorythme.<br>
![donnees.png](donnees.png) <br>

Preuve que l'attaque ne fonctionne plus : <br>
1. Dans l'application, utiliser l'option "Les premiers ministres" et créer un utilisateur avec le NAS 123456789.
   ![app.png](app.png) <br>
2. Rafraichir la table MUtilisateur.
   ![mutils.png](mutils.png) <br>
Il est maintenant impossible de créer une table de traduction. Sans la clé, l'attaquant n'est pas en mesure de décrypter les NAS.

## Attaque 3 Injection SQL

1. Dans l'application, créer un utilisateur test et utiliser l'option "Les premiers ministres" pour avoir des données.
   ![creersql.png](creersql.png) <br>
2. Choisir l'option connexion et entrer la requête suivante: "'; DROP TABLE IF EXISTS MUtilisateur;    --" avec un mot de passe quelconque. La fonction de l'application ne fonctionnera pas mais la requête va supprimer la table MUtilisateurs puisque le commentaire "--" permet d'ignorer la fin de la première requête. <br>
   ![injection.png](injection.png) <br>
3. Rafraichir la table MUtilisateur et constater que nous avons un message d'erreur puisqu'elle n'existe plus. <br>
   ![erreurtable.png](erreurtable.png) <br>
4. Pour changer le mot de passe de Justin Trudeau, entrer la requête suivante dans l'application : "';  UPDATE MUtilisateur SET motDePasse = 'Passw0rd' WHERE nom = 'Justin Trudeau';  --"
   ![trudeau.png](trudeau.png) <br>
5. Rafraichir la table MUtilisateur et constater que le mot de passe de Justin Trudeau a été changé.
   ![trudeaumdp.png](trudeaumdp.png) <br>

### Correctif implanté

Description du correctif.

Preuve que l'attaque ne fonctionne plus avec étapes + copie d'écran
