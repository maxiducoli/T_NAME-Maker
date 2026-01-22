# Creador de archivo T_NAME — by CARP

🏟️ *Generador de nombres de estadios y equipos para la pantalla de presentación de partidos en Winning Eleven 2002.*

Esta herramienta crea el archivo **`T_NAME.BIN`**, usado por *Winning Eleven 2002* (versión PC) para mostrar los **nombres de los equipos y del estadio** en la pantalla de introducción previa al partido.  

Permite definir textos personalizados (hasta los límites del motor del juego) y exportarlos en tres formatos listos para usar:
- ✅ **TIM**: formato gráfico nativo del juego (16 o 256 colores)
- ✅ **BMP**: para edición o revisión visual externa
- ✅ **BIN comprimido**: listo para insertar directamente en el juego

Además, el archivo BIN generado es **compatible con SinSala-BIN**, permitiendo integrarlo en paquetes gráficos más amplios.

---

## 🖼️ ¿Qué hace exactamente?

- Recibe como entrada los nombres de:
  - Equipo local
  - Equipo visitante
  - Estadio
- Renderiza esos textos como **imágenes indexadas** usando fuentes compatibles con el estilo del juego.
- Genera:
  - Archivos `.TIM` individuales (gráficos + paletas)
  - Versiones `.BMP` para verificación
  - Un archivo `.BIN` comprimido (`T_NAME.BIN`) listo para reemplazar en la carpeta del juego

> 🔧 El formato y tamaño de los textos respetan las limitaciones visuales de la pantalla original para evitar desbordes o corrupción.

---

## 🔗 Integración con otras herramientas

- El archivo `T_NAME.BIN` generado **puede ser editado o reempaquetado** con **[SinSala-BIN](https://github.com/maxiducoli/SinSala-BIN-2k24---by-CARP)**.
- Los `.BMP` resultantes pueden editarse con **La Pinta** (tu editor de píxeles) si se desea ajustar manualmente algún detalle.
- Ideal para usar junto con el **Creador de Estadios** y el **Nombres Largos** para una experiencia completamente personalizada.

---

## 💻 Tecnología

- **Lenguaje**: C#  
- **Framework**: .NET (Windows Forms)  
- **Tipo**: Utilidad de renderizado gráfico + empaquetado binario  
- **Plataforma**: Windows (PC)

---

## 🎯 Caso de uso típico

1. Ingresás: *"River Plate"*, *"Boca Juniors"*, *"El Monumental"*.
2. La herramienta genera los gráficos correspondientes.
3. Exportás el `T_NAME.BIN`.
4. Lo copiás a la carpeta del juego… ¡y listo! La pantalla de presentación muestra tus nombres reales.

---

## 🧠 Inspiración

> *"Si iba a jugar el superclásico, quería que el juego lo anunciara como tal… no como 'Team A vs Team B'."*

Este proyecto cierra el círculo visual: desde los escudos hasta la narración, pasando por la presentación. Todo bajo control.

---

## 📜 Licencia

Uso permitido con fines **no comerciales**. Si reutilizás el código o la idea, citá a **Maximiliano Ducoli (CARP)** como autor original.

---

⚽ ¡Que el partido empiece con el nombre que se merece!
