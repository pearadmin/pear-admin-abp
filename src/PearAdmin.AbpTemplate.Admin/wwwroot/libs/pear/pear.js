window.rootPath = (function (src) {
	src = document.scripts[document.scripts.length - 1].src;
	return src.substring(0, src.lastIndexOf("/") + 1);
})();

layui.config({
	base: rootPath + "module/",
	version: true
}).extend({
	abp: "abp",
	abpcore: "abpcore",
	abpjquery: "abpjquery",
	abpnotice: "abpnotice",
	abpmessage: "abpmessage",
	abpsignalr: "abpsignalr",
	abpsignalrclient: "abpsignalrclient",
	abpsignalrchat: "abpsignalrchat",
	admin: "admin",
	menu: "menu",
	frame: "frame",
	tab: "tab",
	echarts: "echarts",
	echartsTheme: "echartsTheme",
	hash: "hash",
	select: "select",
	drawer: "drawer",
	social: "social",
	step: "step",
	tag: "tag",
	popup: "popup",
	iconPicker: "iconPicker",
	treetable: "treetable",
	dtree: "dtree",
	tinymce: "tinymce/tinymce",
	area: "area",
	count: "count",
	topBar: "topBar",
	button: "button",
	design: "design",
	dropdown: "dropdown",
	card: "card",
	loading: "loading",
	cropper: "cropper"
});
