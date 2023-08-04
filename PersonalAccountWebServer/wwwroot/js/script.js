// const format = function (str, args) {
//     let formatted = str;
//     for (let i = 0; i < args.length; i++) {
//         let regexp = new RegExp('\\{' + i + '\\}', 'gi');
//         formatted = formatted.replace(regexp, args[i]);
//     }
//     return formatted;
// };
//
// function editAccountFormatter(id) {
//     return format(`<a href="/PersonalAccounts/Edit?id={0}">Редактировать</a> | <a href="/PersonalAccounts/Details?id={0}">Подробности</a> | <a href="/PersonalAccounts/Delete?id={0}">Удалить</a>`, [id]);
// }
// function editResidentFormatter(id) {
//     return format(`<a href="/Residents/Edit?id={0}">Редактировать</a> | <a href="/Residents/Details?id={0}">Подробности</a> | <a href="/Residents/Delete?id={0}">Удалить</a>`, [id]);
// }
// function residentFormatter(value) {
//     return `<a href="/Residents/ResidentsByRoom?id=${value}">Проживающие</a>`;
// }
//
// var $table = $('#table');
// $(function () {
//     $('#toolbar').find('select').change(function () {
//         $table.bootstrapTable('refreshOptions', {
//             exportDataType: $(this).val()
//         });
//     });
// })
//
// var trBoldBlue = $("table");
//
// $(trBoldBlue).on("click", "tr", function () {
//     $(this).toggleClass("bold-blue");
// });
