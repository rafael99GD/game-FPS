# FPS - Rafael Sánchez Fernández

![FPS Preview](Assets/preview.png)

**FPS - Rafael Sánchez Fernández** es un juego de disparos en primera persona con un campo de pruebas completo para probar mecánicas, armas y enemigos. Está desarrollado en Unity y sirve como proyecto de demostración para mostrar habilidades en programación de juegos, IA, físicas y sistemas de jugador.

---

## 🎮 Mecánicas del juego

- **Jugador:**
  - Movimiento completo con rig de jugador FPS.
  - Control de vida y monedas.
  - Uso de diferentes armas: pistola, fusil y francotirador.
  - Lanzamiento de granadas con físicas que afectan objetos de la escena.
  - Items que aumentan vida, daño o velocidad de movimiento.
  - Interacción con zonas especiales:
    - Zona de ralentización.
    - Zona de curación.
    - Zona de veneno.

- **Enemigos:**
  - IA que sigue un path predeterminado hasta detectar al jugador.
  - Persiguen al jugador hasta cierto rango; si te sales de ese rango, regresan a su path.
  - Algunos enemigos disparan hacia el jugador.
  
- **Otras mecánicas:**
  - Minimap funcional para pruebas.
  - Sistema de físicas y detección de colisiones.
  - Sistema de pickups y mejoras en el campo de pruebas.

---

## 🛠️ Tecnologías usadas

- Unity 2021/2022 (dependiendo de tu versión)
- C# para scripts
- IA básica para enemigos con pathfinding y detección del jugador
- Física de granadas y objetos
- Sistema de zonas con efectos sobre el jugador
- Minimap funcional

---

## 📂 Contenido del repositorio

- `Assets/` → Todos los assets del juego, incluyendo scripts, prefabs y materiales.  
- `Packages/` → Paquetes de Unity utilizados.  
- `ProjectSettings/` → Configuración del proyecto.  
- `.gitignore` → Ignora archivos temporales y builds.  
- `.gitattributes` → Configuración de Git LFS para archivos grandes.

---

## 🚀 Cómo abrir el proyecto

1. Clona el repositorio:
   ```bash
   git clone https://github.com/rafael99GD/game-FPS.git
2. Abre Unity Hub → Add Project → selecciona la carpeta clonada.
3. Espera a que Unity importe todos los assets.
4. Ejecuta el juego desde Play en el Editor.
