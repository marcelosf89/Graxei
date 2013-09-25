using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using TwitterBootstrapMVC.TypeExtensions;

namespace TwitterBootstrapMVC.Infrastructure
{
    public static class BootstrapHelper
    {
        public static string GetClassForButtonSize(ButtonSize buttonSize)
        {
            if (buttonSize == ButtonSize.Large) return "btn-lg";
            if (buttonSize == ButtonSize.Small) return "btn-sm";
            if (buttonSize == ButtonSize.Mini) return "btn-xs";
            return string.Empty;
        }

        public static string GetClassForButtonStyle(ButtonStyle buttonStyle)
        {
            if (buttonStyle == ButtonStyle.Primary) return "btn-primary";
            if (buttonStyle == ButtonStyle.Info) return "btn-info";
            if (buttonStyle == ButtonStyle.Success) return "btn-success";
            if (buttonStyle == ButtonStyle.Warning) return "btn-warning";
            if (buttonStyle == ButtonStyle.Danger) return "btn-danger";
            if (buttonStyle == ButtonStyle.Inverse) return "btn-inverse";
            if (buttonStyle == ButtonStyle.Link) return "btn-link";
            return "btn-default";
        }

        public static string GetClassForInputSize(InputSize size)
        {
            if (size == InputSize.Mini) return "input-mini";
            if (size == InputSize.BlockLevel) return "input-block-level";
            if (size == InputSize.Small) return "input-sm";
            if (size == InputSize.Medium) return "input-medium";
            if (size == InputSize.Large) return "input-lge";
            if (size == InputSize.XLarge) return "input-xlarge";
            if (size == InputSize.XXLarge) return "input-xxlarge";
            return string.Empty;
        }

        public static string GetClassForInputWidth(InputWidth width)
        {
            if (width == InputWidth.Col_1) return "col-md-1";
            if (width == InputWidth.Col_2) return "col-md-2";
            if (width == InputWidth.Col_3) return "col-md-3";
            if (width == InputWidth.Col_4) return "col-md-4";
            if (width == InputWidth.Col_5) return "col-md-5";
            if (width == InputWidth.Col_6) return "col-md-6";
            if (width == InputWidth.Col_7) return "col-md-7";
            if (width == InputWidth.Col_8) return "col-md-8";
            if (width == InputWidth.Col_9) return "col-md-9";
            if (width == InputWidth.Col_10) return "col-md-10";
            if (width == InputWidth.Col_11) return "col-md-11";
            if (width == InputWidth.Col_12) return "col-md-12";

            if (width == InputWidth.Col_Lg_1) return "col-lg-1";
            if (width == InputWidth.Col_Lg_2) return "col-lg-2";
            if (width == InputWidth.Col_Lg_3) return "col-lg-3";
            if (width == InputWidth.Col_Lg_4) return "col-lg-4";
            if (width == InputWidth.Col_Lg_5) return "col-lg-5";
            if (width == InputWidth.Col_Lg_6) return "col-lg-6";
            if (width == InputWidth.Col_Lg_7) return "col-lg-7";
            if (width == InputWidth.Col_Lg_8) return "col-lg-8";
            if (width == InputWidth.Col_Lg_9) return "col-lg-9";
            if (width == InputWidth.Col_Lg_10) return "col-lg-10";
            if (width == InputWidth.Col_Lg_11) return "col-lg-11";
            if (width == InputWidth.Col_Lg_12) return "col-lg-12";
            return string.Empty;
        }

        public static string GetPlaceholderFromMetadata(ModelMetadata metadata)
        {
            if (metadata == null) return string.Empty;
            if (!string.IsNullOrEmpty(metadata.Watermark)) return metadata.Watermark;
            if (!string.IsNullOrEmpty(metadata.DisplayName)) return metadata.DisplayName;
            if (!string.IsNullOrEmpty(metadata.PropertyName)) return metadata.PropertyName.SplitByUpperCase();
            return string.Empty;
        }

        public static string GetHelpTextFromMetadata(ModelMetadata metadata)
        {
            if (metadata == null) return string.Empty;
            if (!string.IsNullOrEmpty(metadata.Description)) return metadata.Description;
            return string.Empty;
        }
    }
}
