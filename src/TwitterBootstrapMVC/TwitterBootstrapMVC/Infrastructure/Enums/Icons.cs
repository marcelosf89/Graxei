using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace TwitterBootstrapMVC
{
    public enum Icons
    {
        _not_set,
        [Description("glyphicon glyphicon-glass")]
        glass,
        [Description("glyphicon glyphicon-music")]
        music,
        [Description("glyphicon glyphicon-search")]
        search,
        [Description("glyphicon glyphicon-envelope")]
        envelope,
        [Description("glyphicon glyphicon-heart")]
        heart,
        [Description("glyphicon glyphicon-star")]
        star,
        [Description("glyphicon glyphicon-star-empty")]
        star_empty,
        [Description("glyphicon glyphicon-user")]
        user,
        [Description("glyphicon glyphicon-film")]
        film,
        [Description("glyphicon glyphicon-th-large")]
        th_large,
        [Description("glyphicon glyphicon-th")]
        th,
        [Description("glyphicon glyphicon-th-list")]
        th_list,
        [Description("glyphicon glyphicon-ok")]
        ok,
        [Description("glyphicon glyphicon-remove")]
        remove,
        [Description("glyphicon glyphicon-zoom-in")]
        zoom_in,
        [Description("glyphicon glyphicon-zoom-out")]
        zoom_out,
        [Description("glyphicon glyphicon-off")]
        off,
        [Description("glyphicon glyphicon-signal")]
        signal,
        [Description("glyphicon glyphicon-cog")]
        cog,
        [Description("glyphicon glyphicon-trash")]
        trash,
        [Description("glyphicon glyphicon-home")]
        home,
        [Description("glyphicon glyphicon-file")]
        file,
        [Description("glyphicon glyphicon-time")]
        time,
        [Description("glyphicon glyphicon-road")]
        road,
        [Description("glyphicon glyphicon-download-alt")]
        download_alt,
        [Description("glyphicon glyphicon-download")]
        download,
        [Description("glyphicon glyphicon-upload")]
        upload,
        [Description("glyphicon glyphicon-inbox")]
        inbox,
        [Description("glyphicon glyphicon-play-circle")]
        play_circle,
        [Description("glyphicon glyphicon-repeat")]
        repeat,
        [Description("glyphicon glyphicon-refresh")]
        refresh,
        [Description("glyphicon glyphicon-list-alt")]
        list_alt,
        [Description("glyphicon glyphicon-lock")]
        g_lock,
        [Description("glyphicon glyphicon-flag")]
        flag,
        [Description("glyphicon glyphicon-headphones")]
        headphones,
        [Description("glyphicon glyphicon-volume-off")]
        volume_off,
        [Description("glyphicon glyphicon-volume-down")]
        volume_down,
        [Description("glyphicon glyphicon-volume-up")]
        volume_up,
        [Description("glyphicon glyphicon-qrcode")]
        qrcode,
        [Description("glyphicon glyphicon-barcode")]
        barcode,
        [Description("glyphicon glyphicon-tag")]
        tag,
        [Description("glyphicon glyphicon-tags")]
        tags,
        [Description("glyphicon glyphicon-book")]
        book,
        [Description("glyphicon glyphicon-bookmark")]
        bookmark,
        [Description("glyphicon glyphicon-print")]
        print,
        [Description("glyphicon glyphicon-camera")]
        camera,
        [Description("glyphicon glyphicon-font")]
        font,
        [Description("glyphicon glyphicon-bold")]
        bold,
        [Description("glyphicon glyphicon-italic")]
        italic,
        [Description("glyphicon glyphicon-text-height")]
        text_height,
        [Description("glyphicon glyphicon-text-width")]
        text_width,
        [Description("glyphicon glyphicon-align-left")]
        align_left,
        [Description("glyphicon glyphicon-align-center")]
        align_center,
        [Description("glyphicon glyphicon-align-right")]
        align_right,
        [Description("glyphicon glyphicon-align-justify")]
        align_justify,
        [Description("glyphicon glyphicon-list")]
        list,
        [Description("glyphicon glyphicon-indent-left")]
        indent_left,
        [Description("glyphicon glyphicon-indent-right")]
        indent_right,
        [Description("glyphicon glyphicon-facetime-video")]
        facetime_video,
        [Description("glyphicon glyphicon-picture")]
        picture,
        [Description("glyphicon glyphicon-pencil")]
        pencil,
        [Description("glyphicon glyphicon-map-marker")]
        map_marker,
        [Description("glyphicon glyphicon-adjust")]
        adjust,
        [Description("glyphicon glyphicon-tint")]
        tint,
        [Description("glyphicon glyphicon-edit")]
        edit,
        [Description("glyphicon glyphicon-share")]
        share,
        [Description("glyphicon glyphicon-check")]
        check,
        [Description("glyphicon glyphicon-move")]
        move,
        [Description("glyphicon glyphicon-step-backward")]
        step_backward,
        [Description("glyphicon glyphicon-fast-backward")]
        fast_backward,
        [Description("glyphicon glyphicon-backward")]
        backward,
        [Description("glyphicon glyphicon-play")]
        play,
        [Description("glyphicon glyphicon-pause")]
        pause,
        [Description("glyphicon glyphicon-stop")]
        stop,
        [Description("glyphicon glyphicon-forward")]
        forward,
        [Description("glyphicon glyphicon-fast-forward")]
        fast_forward,
        [Description("glyphicon glyphicon-step-forward")]
        step_forward,
        [Description("glyphicon glyphicon-eject")]
        eject,
        [Description("glyphicon glyphicon-chevron-left")]
        chevron_left,
        [Description("glyphicon glyphicon-chevron-right")]
        chevron_right,
        [Description("glyphicon glyphicon-plus-sign")]
        plus_sign,
        [Description("glyphicon glyphicon-minus-sign")]
        minus_sign,
        [Description("glyphicon glyphicon-remove-sign")]
        remove_sign,
        [Description("glyphicon glyphicon-ok-sign")]
        ok_sign,
        [Description("glyphicon glyphicon-question-sign")]
        question_sign,
        [Description("glyphicon glyphicon-info-sign")]
        info_sign,
        [Description("glyphicon glyphicon-screenshot")]
        screenshot,
        [Description("glyphicon glyphicon-remove-circle")]
        remove_circle,
        [Description("glyphicon glyphicon-ok-circle")]
        ok_circle,
        [Description("glyphicon glyphicon-ban-circle")]
        ban_circle,
        [Description("glyphicon glyphicon-arrow-left")]
        arrow_left,
        [Description("glyphicon glyphicon-arrow-right")]
        arrow_right,
        [Description("glyphicon glyphicon-arrow-up")]
        arrow_up,
        [Description("glyphicon glyphicon-arrow-down")]
        arrow_down,
        [Description("glyphicon glyphicon-share-alt")]
        share_alt,
        [Description("glyphicon glyphicon-resize-full")]
        resize_full,
        [Description("glyphicon glyphicon-resize-small")]
        resize_small,
        [Description("glyphicon glyphicon-plus")]
        plus,
        [Description("glyphicon glyphicon-minus")]
        minus,
        [Description("glyphicon glyphicon-asterisk")]
        asterisk,
        [Description("glyphicon glyphicon-exclamation-sign")]
        exclamation_sign,
        [Description("glyphicon glyphicon-gift")]
        gift,
        [Description("glyphicon glyphicon-leaf")]
        leaf,
        [Description("glyphicon glyphicon-fire")]
        fire,
        [Description("glyphicon glyphicon-eye-open")]
        eye_open,
        [Description("glyphicon glyphicon-eye-close")]
        eye_close,
        [Description("glyphicon glyphicon-warning-sign")]
        warning_sign,
        [Description("glyphicon glyphicon-plane")]
        plane,
        [Description("glyphicon glyphicon-calendar")]
        calendar,
        [Description("glyphicon glyphicon-random")]
        random,
        [Description("glyphicon glyphicon-comment")]
        comment,
        [Description("glyphicon glyphicon-magnet")]
        magnet,
        [Description("glyphicon glyphicon-chevron-up")]
        chevron_up,
        [Description("glyphicon glyphicon-chevron-down")]
        chevron_down,
        [Description("glyphicon glyphicon-retweet")]
        retweet,
        [Description("glyphicon glyphicon-shopping-cart")]
        shopping_cart,
        [Description("glyphicon glyphicon-folder-close")]
        folder_close,
        [Description("glyphicon glyphicon-folder-open")]
        folder_open,
        [Description("glyphicon glyphicon-resize-vertical")]
        resize_vertical,
        [Description("glyphicon glyphicon-resize-horizontal")]
        resize_horizontal,
        [Description("glyphicon glyphicon-hdd")]
        hdd,
        [Description("glyphicon glyphicon-bullhorn")]
        bullhorn,
        [Description("glyphicon glyphicon-bell")]
        bell,
        [Description("glyphicon glyphicon-certificate")]
        certificate,
        [Description("glyphicon glyphicon-thumbs-up")]
        thumbs_up,
        [Description("glyphicon glyphicon-thumbs-down")]
        thumbs_down,
        [Description("glyphicon glyphicon-hand-right")]
        hand_right,
        [Description("glyphicon glyphicon-hand-left")]
        hand_left,
        [Description("glyphicon glyphicon-hand-up")]
        hand_up,
        [Description("glyphicon glyphicon-hand-down")]
        hand_down,
        [Description("glyphicon glyphicon-circle-arrow-right")]
        circle_arrow_right,
        [Description("glyphicon glyphicon-circle-arrow-left")]
        circle_arrow_left,
        [Description("glyphicon glyphicon-circle-arrow-up")]
        circle_arrow_up,
        [Description("glyphicon glyphicon-circle-arrow-down")]
        circle_arrow_down,
        [Description("glyphicon glyphicon-globe")]
        globe,
        [Description("glyphicon glyphicon-wrench")]
        wrench,
        [Description("glyphicon glyphicon-tasks")]
        tasks,
        [Description("glyphicon glyphicon-filter")]
        filter,
        [Description("glyphicon glyphicon-briefcase")]
        briefcase,
        [Description("glyphicon glyphicon-fullscreen")]
        fullscreen
    }
}
