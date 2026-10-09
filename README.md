# Schedule I Translate

Traducción de **Schedule I (IL2CPP)**: español sin conexión o traducción automática a otros idiomas mientras juegas. Funciona con **MelonLoader**.

**[Descargar](https://github.com/SasoIkuyo/schedule-i-translate/releases)** · [Código fuente](https://github.com/SasoIkuyo/schedule-i-translate/tree/source) · [Reportar un problema](https://github.com/SasoIkuyo/schedule-i-translate/issues) · [Apoyar el proyecto ☕](https://buymeacoffee.com/sasoikuyo)

[Español](#español) · [English](#english) · [Historial de cambios / Changelog](#historial-de-cambios--changelog)

> Las versiones actuales son preliminares. La traducción sigue en desarrollo y puede haber textos pendientes o ajustes visuales por corregir.

## Español

### Elige una versión

| Versión | Qué ofrece | Conexión | Archivo |
| --- | --- | --- | --- |
| **Offline** | 11.757 entradas integradas en español | No necesita internet | `ScheduleISpanish.dll` |
| **Online** | Diccionario español y traducción automática de textos nuevos; idioma configurable | Necesaria para traducir textos nuevos | `ScheduleTranslate.dll` |

En Online, los textos se traducen conforme aparecen en el juego. En español se aprovecha el diccionario integrado; los otros idiomas parten del texto inglés. Las traducciones obtenidas se guardan en una caché por idioma y se reutilizan en futuras partidas.

### Instalación

Necesitas **Schedule I (IL2CPP)** y **MelonLoader** instalado. La compatibilidad con nuevas actualizaciones del juego debe comprobarse.

1. Cierra el juego.
2. Descarga **un solo ZIP**, Online u Offline, desde [Releases](https://github.com/SasoIkuyo/schedule-i-translate/releases).
3. Retira las otras DLL de traducción activas y la versión anterior de este mod.
4. Extrae el ZIP y copia su carpeta `Mods` en la carpeta del juego, junto a `Schedule I.exe`.
5. Inicia el juego normalmente.

Instala **solo una variante**. Los ZIP contienen únicamente la DLL del mod; MelonLoader se instala por separado. No necesitas ejecutar scripts ni PowerShell.

**Al actualizar:** conserva `UserData/ScheduleISpanish` para mantener tus ajustes y traducciones guardadas. Si tienes el prototipo antiguo, retira también `ScheduleSpanishOffline.dll`.

### Configuración

Los archivos de configuración se crean al iniciar el mod. Cierra el juego antes de editarlos y reinícialo para aplicar los cambios.

#### Idioma de Online

Edita `UserData/ScheduleISpanish/language.json`. Por ejemplo, para francés:

```json
{
  "TargetLanguage": "fr"
}
```

| Código | Idioma | Código | Idioma |
| --- | --- | --- | --- |
| `es` | Español, predeterminado | `it` | Italiano |
| `fr` | Francés | `ja` | Japonés |
| `de` | Alemán | `ko` | Coreano |
| `pt` | Portugués | `zh-CN` | Chino simplificado |
| `en` | Inglés original, sin consultas | | |

Estos son ejemplos de códigos admitidos por Google Translate. La selección de idioma solo afecta a Online. Cada idioma usa su propio archivo de caché, como `cache.fr.jsonl`, dentro de `UserData/ScheduleISpanish`.

#### Texto del teléfono

El mod utiliza una fuente existente del juego para mejorar la legibilidad y ajusta el espacio de los rótulos e importes. Si una etiqueta contiene caracteres que esa fuente no admite, conserva la fuente original.

Para restaurar por completo el aspecto original del texto, edita `UserData/ScheduleISpanish/display.json`:

```json
{
  "PhoneTextScale": 1,
  "UseSdfPhoneText": false
}
```

Los valores predeterminados son `PhoneTextScale: 2` y `UseSdfPhoneText: true`. No se aumenta la resolución de toda la escena ni se instalan fuentes externas. Las mejoras visuales todavía necesitan comprobación dentro del juego.

### Cómo funciona Online

- Envía los textos nuevos a Google desde la conexión de cada jugador.
- Procesa las consultas una por una, con una cola limitada, y guarda los resultados localmente.
- Reutiliza las traducciones guardadas y excluye contadores FPS, versiones, importes aislados y horas aisladas de las consultas.
- No incluye claves API, credenciales del autor ni un proyecto de facturación de Google Cloud.

Google puede limitar o rechazar solicitudes. Si falla la conexión, las traducciones locales siguen disponibles y los textos nuevos pueden permanecer en inglés.

### Cobertura y problemas conocidos

La traducción combina resultados automáticos y correcciones de vocabulario según el contexto del juego. Todavía pueden quedar frases dinámicas o textos dentro de imágenes sin traducir. Algunas fuentes no incluyen los caracteres de ciertos idiomas.

No se ha verificado la cobertura completa de todos los idiomas ni el funcionamiento en todas las versiones del juego o equipos. Los ajustes de distribución del teléfono y de las opciones de diálogo siguen pendientes de validación visual.

### Reportar un problema

Abre un [issue](https://github.com/SasoIkuyo/schedule-i-translate/issues) e incluye:

- Versión del juego y variante del mod: Online u Offline.
- Idioma elegido y captura o texto afectado.
- `MelonLoader/Latest.log` si hay errores de carga o cierres. Revisa el registro y elimina datos personales antes de compartirlo.

### Código fuente y apoyo

El código y las pruebas están en la rama [source](https://github.com/SasoIkuyo/schedule-i-translate/tree/source). Las instrucciones para compilar están en [BUILD.md](https://github.com/SasoIkuyo/schedule-i-translate/blob/source/BUILD.md).

Si te sirve la traducción, puedes apoyar su desarrollo en [Buy Me a Coffee](https://buymeacoffee.com/sasoikuyo).

## English

Translation for **Schedule I (IL2CPP)** using **MelonLoader**, with offline Spanish and automatic translation into configurable languages while you play.

### Choose a version

| Version | Features | Connection | File |
| --- | --- | --- | --- |
| **Offline** | 11,757 embedded Spanish entries | Not required | `ScheduleISpanish.dll` |
| **Online** | Spanish dictionary, translation of new text and configurable target language | Required for new translations | `ScheduleTranslate.dll` |

Online translates labels as they appear. Spanish uses the embedded dictionary; other languages start from the English text. Results are saved in a separate cache for each language and reused across sessions.

### Installation and updates

1. Install **MelonLoader** for your IL2CPP game installation.
2. Close the game and download **one ZIP** from [Releases](https://github.com/SasoIkuyo/schedule-i-translate/releases).
3. Remove other active translation DLLs and the previous version of this mod.
4. Extract the ZIP and copy its `Mods` folder next to `Schedule I.exe`.
5. Start the game normally.

Install **only one variant**. Packages contain only the mod DLL; no scripts or PowerShell are needed. Keep `UserData/ScheduleISpanish` when updating to preserve settings and cached translations. Remove `ScheduleSpanishOffline.dll` if you used the old prototype. Compatibility with new game updates needs verification.

### Configuration

Launch the mod once to create its settings, then close the game before editing them. Restart to apply changes.

**Online language:** edit `UserData/ScheduleISpanish/language.json`:

```json
{
  "TargetLanguage": "fr"
}
```

Examples: `es` Spanish (default), `fr` French, `de` German, `pt` Portuguese, `it` Italian, `ja` Japanese, `ko` Korean, `zh-CN` Simplified Chinese, or `en` for original English without translation requests. Use a language code supported by Google Translate. Caches such as `cache.fr.jsonl` are stored in `UserData/ScheduleISpanish`.

**Phone text:** the mod uses an existing game font and adjusts label spacing. Unsupported characters retain the original font. To restore the original appearance, edit `UserData/ScheduleISpanish/display.json`:

```json
{
  "PhoneTextScale": 1,
  "UseSdfPhoneText": false
}
```

Defaults are `PhoneTextScale: 2` and `UseSdfPhoneText: true`. No external fonts or scene resolution changes are included. Visual results still need in-game checking.

### Network and coverage

Online sends new text to Google using each player's connection. Requests run one at a time with a bounded queue and persistent caching. FPS counters, version strings, standalone currency and clock values are excluded. No API keys, author credentials or Google Cloud billing project are embedded.

The service may limit or reject requests. Local translations remain available when the connection fails; new text may stay in English. Translations combine automatic results and contextual corrections. Dynamic text, text inside images and unsupported font characters remain possible gaps. Full language coverage, visual layout and compatibility with every game version or computer have not been verified.

### Reports, source and support

Report problems through [Issues](https://github.com/SasoIkuyo/schedule-i-translate/issues), including the game version, mod variant, language and affected text or screenshot. Attach `MelonLoader/Latest.log` for crashes or load errors, removing personal information first.

Source and tests: [source branch](https://github.com/SasoIkuyo/schedule-i-translate/tree/source) · [Build instructions](https://github.com/SasoIkuyo/schedule-i-translate/blob/source/BUILD.md) · [Support development ☕](https://buymeacoffee.com/sasoikuyo)

## Historial de cambios / Changelog

| Versión / Version | Cambios principales | Main changes |
| --- | --- | --- |
| [1.4.1](https://github.com/SasoIkuyo/schedule-i-translate/releases/tag/v1.4.1) | Espacios separados para opciones y requisitos; avisos de región cortos y mayúsculas corregidas. | Separate option and requirement bounds; shorter region notices and corrected capitalization. |
| [1.4.0](https://github.com/SasoIkuyo/schedule-i-translate/releases/tag/v1.4.0) | Fuente del teléfono, ajuste de pedidos y vocabulario de entregas. | Phone font, supplier order fitting and delivery terminology. |
| [1.3.2](https://github.com/SasoIkuyo/schedule-i-translate/releases/tag/v1.3.2) | Filtro de FPS, reintentos de consultas y traducción de patinetas con precios. | FPS filtering, request retries and priced skateboard labels. |
| [1.3.1](https://github.com/SasoIkuyo/schedule-i-translate/releases/tag/v1.3.1) | Distribución del menú de partidas, tiempo restante y mensajes dinámicos del teléfono. | Save menu layout, remaining time and dynamic phone messages. |
| [1.3.0](https://github.com/SasoIkuyo/schedule-i-translate/releases/tag/v1.3.0) | Bloques de misiones, diálogos variables y correcciones de cultivo y nombres. | Mission blocks, variable dialogue and cultivation/name corrections. |
