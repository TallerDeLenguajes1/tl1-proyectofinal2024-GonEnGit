# Proyecto taller 1 - 2024 - 'Todo esto por un trono?'

## Descripcion
'Todo esto por un trono?' es un mini-rouge lite donde te enfrentaras a 9 rivales minetras lees la historia
de dos personajes intentando llevar adelante el juego de la peor forma posible, olivdandose partes de la historia,
haciendo las cosas de forma desordenada o simplemente peleandose entre ellos.

## Jugabilidad
El juego es simple, tendras que elegir tu personaje y luego intentar ganar 9 duelos en un combate por turnos.

Antes de cada duelos, podras intentar decidir si quieres mover antes que tu enemigo o mover despues para 
saber de antemano que es lo que quiere hacer en este turno. Esto lo podras lograr eligiendo una de dos
cartas para decidir tu iniciativa, puedes saber el valor que tendrá tu eleccion siguiente la siguiente regla:
- El color de la carta multiplica su valro: Azul = x2, Amarillo = x1.5
- Mientras que el simbolo que se muestra en la carta suma un valor adicinal: ♠ = +2, ♣ = +3, ♦ = +4, ♥ = +5

Despues de cada duelo -si ganas- tu personaje subirá de nivel y ademas podras elegir una mejora para tu personaje.
Pero tu personaje no es el unico que puede mejorar, los niveles de tus enemigos tambien incremetan segun que tan lejos
estes en la historia.

## API utilizada

'Deck of Cards', una API ocn varias funcionalidades para simular el uso de un mazo de cartas de poker

En este caso se usaron:
- [Una funcionalidad para generar un mazo nuevo:](http://deckofcardsapi.com/api/deck/new/shuffle/?deck_count=1)
- [Una funcionalidad para extraer cartas de ese mazo:](https://deckofcardsapi.com/api/deck/<IdMazo>/draw/?count=2)