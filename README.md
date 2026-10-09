# Schedule I Translate

## Teléfono 1.4.0 / Phone readability

El teléfono usa la fuente SDF existente en el juego, ajuste de texto y espacios separados para rótulos e importes en el pedido del proveedor. Los nombres largos de pseudoefedrina tienen una forma corta solo en el teléfono. Se corrigen «entrega oculta», «Pedir mercancía» y las horas de preparación. La comprobación visual dentro del juego está pendiente.

Puedes restaurar la fuente original cerrando el juego y añadiendo `"UseSdfPhoneText": false` a `UserData/ScheduleISpanish/display.json`. El ajuste se activa por defecto, también con configuraciones anteriores. Si la fuente SDF no admite los caracteres, esa etiqueta conserva la fuente original. No se aumenta la resolución de la escena ni se incluyen fuentes externas.

Phone labels use the game's existing SDF font and text fitting. Supplier order captions and amounts have separate bounds. Long pseudoephedrine names use phone-only display aliases; delivery vocabulary and preparation durations are corrected. In-game visual verification is pending. Set `"UseSdfPhoneText": false` in `UserData/ScheduleISpanish/display.json` to restore the original font. Labels containing unsupported characters retain their original font. No external fonts or scene resolution changes are included.

Traducción para **Schedule I (IL2CPP)**, con una versión offline en español y una versión online con idioma configurable.

[Descargar versiones](https://github.com/SasoIkuyo/schedule-i-translate/releases) · [Apoyar el desarrollo ☕](https://buymeacoffee.com/sasoikuyo)

> Las versiones actuales se publican como preliminares. Encuentra los paquetes en [Releases](https://github.com/SasoIkuyo/schedule-i-translate/releases).

## Español

### Requisitos y versiones

Referencias de compilación: 0.4.6f13 y 0.4.7f12. No es una restricción a esas versiones; la compatibilidad con otras actualizaciones debe comprobarse.

- **MelonLoader 0.7.3**, instalado y compatible con tu juego.
- **Offline:** 11.754 entradas integradas en español; funciona sin consultas de red.
- **Online:** el mismo diccionario español y traducción de textos nuevos mediante Google. Permite elegir otros idiomas y guarda una caché independiente para cada uno.

### Instalación

1. Cierra el juego.
2. Descarga **un solo ZIP**, Online u Offline, desde [Releases](https://github.com/SasoIkuyo/schedule-i-translate/releases).
3. Retira los otros mods de traducción activos para evitar conflictos.
4. Extrae el ZIP y copia su carpeta `Mods` en la carpeta del juego, junto a `Schedule I.exe`.
5. Inicia el juego normalmente.

Online usa `Mods/ScheduleTranslate.dll` y Offline usa `Mods/ScheduleISpanish.dll`: instala solo una. Al actualizar, retira la DLL anterior; el antiguo prototipo se llamaba `ScheduleSpanishOffline.dll`. Si cargas las dos variantes actuales, Online se desactiva y se conserva Offline. No necesitas ejecutar scripts ni PowerShell. Los paquetes contienen únicamente la DLL del mod; MelonLoader se instala por separado.

### Cambiar el idioma online

Inicia el juego una vez con la versión Online y ciérralo. Abre `UserData/ScheduleISpanish/language.json` con un editor de texto y cambia el código:

```json
{
  "TargetLanguage": "fr"
}
```

| Código | Idioma |
| --- | --- |
| `es` | Español, predeterminado |
| `fr` | Francés |
| `de` | Alemán |
| `pt` | Portugués |
| `it` | Italiano |
| `ja` | Japonés |
| `ko` | Coreano |
| `zh-CN` | Chino simplificado |
| `en` | Inglés original, sin consultas de traducción |

Usa un código admitido por Google Translate y reinicia el juego después de modificarlo. Esta configuración solo afecta a Online.

En español se usa el diccionario integrado. Los demás idiomas parten del texto inglés y se traducen de forma asíncrona conforme aparecen las etiquetas. Cada idioma conserva sus resultados en un archivo como `cache.fr.jsonl`, dentro de `UserData/ScheduleISpanish`. Los textos guardados se reutilizan entre partidas sin volver a consultarlos.

### Conexión, rendimiento y limitaciones

Online envía los textos encontrados a Google desde la conexión de cada jugador. El mod no incluye claves API, cuentas del autor ni un proyecto de facturación de Google Cloud. Google puede limitar o rechazar solicitudes; la disponibilidad del servicio no está garantizada.

Las consultas se procesan una por una, con cola limitada y caché persistente. Si falla la conexión, las traducciones locales siguen disponibles y los textos nuevos permanecen en inglés.

La traducción combina resultados automáticos y correcciones locales. **Todavía puede haber textos sin traducir**, especialmente frases dinámicas y texto dibujado en imágenes. Algunas fuentes del juego podrían no mostrar caracteres de ciertos idiomas. No se ha verificado la cobertura completa de cada idioma ni el funcionamiento en todas las versiones del juego o equipos.

### Informar de problemas

Abre un [issue](https://github.com/SasoIkuyo/schedule-i-translate/issues) con la versión del juego, variante del mod, idioma elegido, una captura o el texto afectado y, si hay un cierre o error, `MelonLoader/Latest.log`. Revisa el registro antes de compartirlo y elimina datos personales que pueda contener.

### Apoyar el desarrollo

Si te sirve el mod, puedes apoyar su desarrollo en [Buy Me a Coffee](https://buymeacoffee.com/sasoikuyo).

## English

### Requirements and installation

Translation for **Schedule I (IL2CPP)**. Requires compatible **MelonLoader 0.7.3**, installed separately. Build references: 0.4.6f13 and 0.4.7f12; compatibility with other updates needs verification.

1. Close the game and download **one ZIP** from [Releases](https://github.com/SasoIkuyo/schedule-i-translate/releases).
2. Remove other active translation mods to avoid conflicts.
3. Extract the ZIP and copy its `Mods` folder into the game directory, next to `Schedule I.exe`.
4. Start the game normally. No scripts or PowerShell are needed.

Online uses `Mods/ScheduleTranslate.dll`; Offline uses `Mods/ScheduleISpanish.dll`. Install only one and remove the previous translation DLL when updating (the old prototype was `ScheduleSpanishOffline.dll`). If both current variants are loaded, Online disables itself and Offline remains active. **Offline** provides 11,754 embedded Spanish entries without network requests. **Online** adds translation of new text and a configurable target language.

### Select a language

Launch Online once, close the game, then edit `UserData/ScheduleISpanish/language.json`:

```json
{
  "TargetLanguage": "fr"
}
```

Examples: `es` Spanish (default), `fr` French, `de` German, `pt` Portuguese, `it` Italian, `ja` Japanese, `ko` Korean, `zh-CN` Simplified Chinese, or `en` for original English without translation requests. Use a language code supported by Google Translate and restart after changing it.

Spanish uses the embedded dictionary. Other languages translate encountered English labels asynchronously. Each language has a separate persistent cache, such as `cache.fr.jsonl`. Saved results are reused across sessions without another request.

### Network and coverage

Online sends encountered text to Google using each player's connection. No API key, author account or Google Cloud billing project is embedded. Requests run one at a time with a bounded queue. The service may limit or reject requests. When requests fail, local translations remain available and new labels stay in English.

Translations are automated with local corrections. Dynamic text and text inside images may remain untranslated. Game fonts may lack characters needed by some languages. Complete language coverage and compatibility with every game version or computer have not been verified.

Report problems through [Issues](https://github.com/SasoIkuyo/schedule-i-translate/issues), including your game version, mod variant, language, affected text or screenshot, and `MelonLoader/Latest.log` for crashes. Remove personal information from logs before sharing them.

[Support development on Buy Me a Coffee ☕](https://buymeacoffee.com/sasoikuyo)

## Código fuente / Source code

El código del mod y sus pruebas están en la rama [source](https://github.com/SasoIkuyo/schedule-i-translate/tree/source), con instrucciones de compilación.

The mod source and tests are available on the [source branch](https://github.com/SasoIkuyo/schedule-i-translate/tree/source), with build instructions.


## Cambios 1.3.0 / Changes

Traducción de bloques de misiones y diálogos con nombres de zonas variables. Se conservan nombres de personajes y variedades como Green Crack y Sour Diesel. Se corrigieron términos de cultivo, semillas de marihuana y mayúsculas iniciales de los artículos. El diccionario se actualizó con el inventario de 0.4.7f12.

Para mejorar la legibilidad del teléfono se aumenta la densidad de rasterización del texto clásico de Unity en `AppsCanvas`, manteniendo su tamaño y distribución. La mejora todavía requiere comprobación visual dentro del juego. Puedes desactivarla cerrando el juego y cambiando `UserData/ScheduleISpanish/display.json` a:

```json
{"PhoneTextScale": 1}
```

El valor predeterminado es `2`. No se aumenta la resolución de toda la escena. Los archivos de configuración y caché permanecen en `UserData/ScheduleISpanish` para conservar los ajustes al actualizar.

Version 1.3.0 adds mission block translation and dialogue templates with runtime region names. Character and strain names are preserved, cultivation vocabulary is corrected, and item labels start with capitals. The dictionary includes the 0.4.7f12 inventory.

Phone text rasterization density is increased for legacy Unity labels in `AppsCanvas`, without resizing their layout or raising scene resolution. Visual results still need in-game checking. Set `PhoneTextScale` to `1` in `UserData/ScheduleISpanish/display.json` to disable it (`2` is the default). Settings and caches remain under `UserData/ScheduleISpanish` across upgrades.

## Correcciones 1.3.1 / Fixes

Se separaron los espacios de rótulos y valores en el menú de partidas, con ajuste de tamaño y recorte dentro de cada campo. Se añadieron fechas como Hoy y Ayer y rótulos más cortos. El tiempo restante admite etiquetas de color internas. Los mensajes dinámicos del teléfono admiten precios con formato y contraofertas, conservando cantidades, nombres y horarios. Se traduce la indicación Usar estación de envasado y otras indicaciones de uso de artículos conocidos. La distribución visual del menú requiere comprobación en el juego.

Save-slot captions and values have separate bounds, with text fitting and ellipsis. Relative dates and shorter captions were added. Remaining time handles internal color tags. Phone templates support formatted prices and counteroffers while preserving quantities, names and meeting times. Known-item interaction prompts are translated. The menu layout still requires in-game visual checking.
## Correcciones 1.3.2 / Fixes

Online excluye contadores FPS, versiones, importes aislados y horas aisladas de las consultas. Las etiquetas visibles que llegan con la cola llena se conservan en una lista limitada y se reintentan; se descartan las referencias de etiquetas que ya cambiaron. Los fallos de red se notifican de forma limitada y se reintentan después de la espera existente. Las traducciones nuevas empiezan con mayúscula sin modificar etiquetas de formato.

Los nombres conocidos con precio, incluidos los menús de patinetas, se traducen localmente conservando importes y colores. Se añadieron Hablar con…, el aviso de dormir y correcciones de las opciones de compra.

Online excludes FPS counters, version strings, standalone currency and clock values from requests. Visible labels rejected by a full queue remain in a bounded retry list; stale label references are removed. Network failures produce limited diagnostics and retry after the existing cooldown. New translations capitalize their first visible letter without changing markup. Known item-and-price labels, NPC talk prompts and the bedtime restriction are translated locally.
