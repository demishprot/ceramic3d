# ceramic3d
0. Оценил задачу в 4 часа с учетом постройки DI+MVC архитектуры проекта и разработки основной логики, выполнил за ~3.5 часа.
1. Функция реализована в `MatrixService.SearchOffsets`.
2. Визулизирован алгоритм поиска с помощью `PrimitiveFactory`, `MatrixService.OnMatch`.
3. Экспорт происходит при успешном выполнении `MatrixService.SearchOffsets` с помощью `MatrixService.UploadOffsets` => `OffsetRepository.Set` => `DataSaver.SaveDataAsync`.

Использованные библиотеки: `Zenject`, `UniTask`, `Newtonsoft`.
Проект построен на клиент-серверной архитектуре MVC для возможности расширения функционала.