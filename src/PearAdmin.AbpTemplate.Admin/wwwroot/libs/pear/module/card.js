layui.define(['table', 'laypage', 'jquery', 'element', 'laytpl'], function (exports) {
    "use strict";

    var MOD_NAME = 'card',
        $ = layui.jquery,
        element = layui.element,
        laypage = layui.laypage,
        laytpl = layui.laytpl;

    var pearCard = {
        render: function (opt) {
            var self = this;
            var option = {
                elem: opt.elem,
                url: opt.url,
                template: opt.template,
                lineSize: opt.lineSize ? opt.lineSize : 4,
                layout: opt.layout ? opt.layout : ['count', 'prev', 'page', 'next', 'limit', 'refresh', 'skip'],
                limit: opt.limit ? opt.limit : 12,
                currentPage: opt.currentPage ? opt.currentPage : 1,
                parseData: opt.parseData ? opt.parseData : function (res) {
                    return res;
                },
                done: opt.done ? opt.done : function () {

                }
            }

            if (option.url != null) {
                var data = getData(option.url + "?page=" + option.currentPage + "&limit=" + option.limit);
                option.data = option.parseData(data);
            }

            var html = createComponent(option);
            $(option.elem).html(html);

            laypage.render({
                elem: 'cardpage',
                count: option.data.count,
                limit: option.limit,
                curr: option.currentPage,
                layout: option.layout,
                jump: function (obj, first) {
                    option.limit = obj.limit;
                    option.currentPage = obj.curr;
                    if (!first) {
                        self.render(option);
                    }
                }
            });
        }
    }

    function createComponent(option) {
        var html = "<div class='pear-card-component'>"
        var content = createCards(option);
        var page = "<div id='cardpage'></div>"
        content = content + page;
        html += content + "</div>"
        return html;
    }

    function createCards(option) {
        var content = "<div class='layui-row layui-col-space30'>";
        $.each(option.data.data, function (i, item) {
            laytpl($(option.template).html()).render(item, function (html) {
                content += html;
            });
        });
        content += "</div>"
        return content;
    }

    function getData(url) {

        $.ajaxSettings.async = false;
        var data = null;

        $.get(url, function (result) {
            data = result;
        });

        $.ajaxSettings.async = true;
        return data;
    }

    exports(MOD_NAME, pearCard);
});
