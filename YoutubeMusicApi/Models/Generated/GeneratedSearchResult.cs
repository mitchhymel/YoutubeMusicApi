using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace YoutubeMusicApi.Models.Generated
{
    public class Param
    {
        [JsonProperty("key")]
        public string Key { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }
    }

    public class ServiceTrackingParam
    {
        [JsonProperty("service")]
        public string Service { get; set; }

        [JsonProperty("params")]
        public List<Param> Params { get; set; }
    }

    public class ResponseContext
    {
        [JsonProperty("visitorData")]
        public string VisitorData { get; set; }

        [JsonProperty("maxAgeSeconds")]
        public int MaxAgeSeconds { get; set; }

        [JsonProperty("serviceTrackingParams")]
        public List<ServiceTrackingParam> ServiceTrackingParams { get; set; }
    }

    public class SearchEndpoint
    {
        [JsonProperty("query")]
        public string Query { get; set; }

        [JsonProperty("params")]
        public string Params { get; set; }
    }

    public class Endpoint
    {
        [JsonProperty("clickTrackingParams")]
        public string ClickTrackingParams { get; set; }

        [JsonProperty("searchEndpoint")]
        public SearchEndpoint SearchEndpoint { get; set; }
    }

    public class Run
    {
        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("navigationEndpoint")]
        public NavigationEndpoint NavigationEndpoint { get; set; }

        [JsonProperty("watchEndpoint")]
        public WatchEndpoint WatchEndpoint { get; set; }
    }

    public class Title
    {
        [JsonProperty("runs")]
        public List<Run> Runs { get; set; }
    }

    public class Thumbnail
    {
        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("width")]
        public int Width { get; set; }

        [JsonProperty("height")]
        public int Height { get; set; }
    }

    public class ThumbnailList
    {
        [JsonProperty("thumbnails")]
        public List<Thumbnail> Thumbnails { get; set; }
    }

    public class MusicThumbnailRenderer
    {
        [JsonProperty("thumbnail")]
        public ThumbnailList Thumbnail { get; set; }

        [JsonProperty("thumbnailCrop")]
        public string ThumbnailCrop { get; set; }

        [JsonProperty("thumbnailScale")]
        public string ThumbnailScale { get; set; }

        [JsonProperty("trackingParams")]
        public string TrackingParams { get; set; }
    }

    public class ThumbnailContainer
    {
        [JsonProperty("musicThumbnailRenderer")]
        public MusicThumbnailRenderer MusicThumbnailRenderer { get; set; }
    }

    public class BrowseEndpointContextMusicConfig
    {
        [JsonProperty("pageType")]
        public string PageType { get; set; }
    }

    public class BrowseEndpointContextSupportedConfigs
    {
        [JsonProperty("browseEndpointContextMusicConfig")]
        public BrowseEndpointContextMusicConfig BrowseEndpointContextMusicConfig { get; set; }
    }

    public class BrowseEndpoint
    {
        [JsonProperty("browseId")]
        public string BrowseId { get; set; }

        [JsonProperty("browseEndpointContextSupportedConfigs")]
        public BrowseEndpointContextSupportedConfigs BrowseEndpointContextSupportedConfigs { get; set; }
    }

    public class Text
    {
        [JsonProperty("runs")]
        public List<Run> Runs { get; set; }
    }

    public class MusicResponsiveListItemFlexColumnRenderer
    {
        [JsonProperty("text")]
        public Text Text { get; set; }

        [JsonProperty("displayPriority")]
        public string DisplayPriority { get; set; }
    }

    public class FlexColumn
    {
        [JsonProperty("musicResponsiveListItemFlexColumnRenderer")]
        public MusicResponsiveListItemFlexColumnRenderer MusicResponsiveListItemFlexColumnRenderer { get; set; }
    }

    public class Icon
    {
        [JsonProperty("iconType")]
        public string IconType { get; set; }
    }

    public class WatchPlaylistEndpoint
    {
        [JsonProperty("playlistId")]
        public string PlaylistId { get; set; }

        [JsonProperty("params")]
        public string Params { get; set; }
    }

    public class ShareEntityEndpoint
    {
        [JsonProperty("serializedShareEntity")]
        public string SerializedShareEntity { get; set; }
    }

    public class AddToPlaylistEndpoint
    {
        [JsonProperty("videoId")]
        public string VideoId { get; set; }

        [JsonProperty("playlistId")]
        public string PlaylistId { get; set; }
    }

    public class WatchEndpoint
    {
        [JsonProperty("videoId")]
        public string VideoId { get; set; }

        [JsonProperty("playlistId")]
        public string PlaylistId { get; set; }

        [JsonProperty("params")]
        public string Params { get; set; }

        [JsonProperty("watchEndpointMusicSupportedConfigs")]
        public WatchEndpointMusicSupportedConfigs WatchEndpointMusicSupportedConfigs { get; set; }
    }

    public class NavigationEndpoint
    {
        [JsonProperty("clickTrackingParams")]
        public string ClickTrackingParams { get; set; }

        [JsonProperty("watchPlaylistEndpoint")]
        public WatchPlaylistEndpoint WatchPlaylistEndpoint { get; set; }

        [JsonProperty("shareEntityEndpoint")]
        public ShareEntityEndpoint ShareEntityEndpoint { get; set; }

        [JsonProperty("addToPlaylistEndpoint")]
        public AddToPlaylistEndpoint AddToPlaylistEndpoint { get; set; }

        [JsonProperty("watchEndpoint")]
        public WatchEndpoint WatchEndpoint { get; set; }

        [JsonProperty("browseEndpoint")]
        public BrowseEndpoint BrowseEndpoint { get; set; }

        [JsonProperty("searchEndpoint")]
        public SearchEndpoint SearchEndpoint { get; set; }
    }

    public class MenuNavigationItemRenderer
    {
        [JsonProperty("text")]
        public Text Text { get; set; }

        [JsonProperty("icon")]
        public Icon Icon { get; set; }

        [JsonProperty("navigationEndpoint")]
        public NavigationEndpoint NavigationEndpoint { get; set; }

        [JsonProperty("trackingParams")]
        public string TrackingParams { get; set; }
    }

    public class DefaultText
    {
        [JsonProperty("runs")]
        public List<Run> Runs { get; set; }
    }

    public class DefaultIcon
    {
        [JsonProperty("iconType")]
        public string IconType { get; set; }
    }

    public class FeedbackEndpoint
    {
        [JsonProperty("feedbackToken")]
        public string FeedbackToken { get; set; }
    }

    public class Target
    {
        [JsonProperty("videoId")]
        public string VideoId { get; set; }

        [JsonProperty("playlistId")]
        public string PlaylistId { get; set; }
    }

    public class MusicLibraryStatusUpdateCommand
    {
        [JsonProperty("libraryStatus")]
        public string LibraryStatus { get; set; }

        [JsonProperty("addToLibraryFeedbackToken")]
        public string AddToLibraryFeedbackToken { get; set; }
    }

    public class Action
    {
        [JsonProperty("clickTrackingParams")]
        public string ClickTrackingParams { get; set; }

        [JsonProperty("musicLibraryStatusUpdateCommand")]
        public MusicLibraryStatusUpdateCommand MusicLibraryStatusUpdateCommand { get; set; }
    }

    public class LikeEndpoint
    {
        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("target")]
        public Target Target { get; set; }

        [JsonProperty("actions")]
        public List<Action> Actions { get; set; }
    }

    public class DefaultServiceEndpoint
    {
        [JsonProperty("clickTrackingParams")]
        public string ClickTrackingParams { get; set; }

        [JsonProperty("feedbackEndpoint")]
        public FeedbackEndpoint FeedbackEndpoint { get; set; }

        [JsonProperty("likeEndpoint")]
        public LikeEndpoint LikeEndpoint { get; set; }
    }

    public class ToggledText
    {
        [JsonProperty("runs")]
        public List<Run> Runs { get; set; }
    }

    public class ToggledIcon
    {
        [JsonProperty("iconType")]
        public string IconType { get; set; }
    }

    public class ToggledServiceEndpoint
    {
        [JsonProperty("clickTrackingParams")]
        public string ClickTrackingParams { get; set; }

        [JsonProperty("feedbackEndpoint")]
        public FeedbackEndpoint FeedbackEndpoint { get; set; }

        [JsonProperty("likeEndpoint")]
        public LikeEndpoint LikeEndpoint { get; set; }
    }

    public class ToggleMenuServiceItemRenderer
    {
        [JsonProperty("defaultText")]
        public DefaultText DefaultText { get; set; }

        [JsonProperty("defaultIcon")]
        public DefaultIcon DefaultIcon { get; set; }

        [JsonProperty("defaultServiceEndpoint")]
        public DefaultServiceEndpoint DefaultServiceEndpoint { get; set; }

        [JsonProperty("toggledText")]
        public ToggledText ToggledText { get; set; }

        [JsonProperty("toggledIcon")]
        public ToggledIcon ToggledIcon { get; set; }

        [JsonProperty("toggledServiceEndpoint")]
        public ToggledServiceEndpoint ToggledServiceEndpoint { get; set; }

        [JsonProperty("trackingParams")]
        public string TrackingParams { get; set; }
    }

    public class QueueTarget
    {
        [JsonProperty("videoId")]
        public string VideoId { get; set; }

        [JsonProperty("playlistId")]
        public string PlaylistId { get; set; }
    }

    public class SuccessResponseText
    {
        [JsonProperty("runs")]
        public List<Run> Runs { get; set; }
    }

    public class NotificationTextRenderer
    {
        [JsonProperty("successResponseText")]
        public SuccessResponseText SuccessResponseText { get; set; }

        [JsonProperty("trackingParams")]
        public string TrackingParams { get; set; }
    }

    public class AddToToastAction
    {
        [JsonProperty("item")]
        public Item Item { get; set; }
    }

    public class Command
    {
        [JsonProperty("clickTrackingParams")]
        public string ClickTrackingParams { get; set; }

        [JsonProperty("addToToastAction")]
        public AddToToastAction AddToToastAction { get; set; }
    }

    public class QueueAddEndpoint
    {
        [JsonProperty("queueTarget")]
        public QueueTarget QueueTarget { get; set; }

        [JsonProperty("queueInsertPosition")]
        public string QueueInsertPosition { get; set; }

        [JsonProperty("commands")]
        public List<Command> Commands { get; set; }
    }

    public class GetReportFormEndpoint
    {
        [JsonProperty("params")]
        public string Params { get; set; }
    }

    public class ServiceEndpoint
    {
        [JsonProperty("clickTrackingParams")]
        public string ClickTrackingParams { get; set; }

        [JsonProperty("queueAddEndpoint")]
        public QueueAddEndpoint QueueAddEndpoint { get; set; }

        [JsonProperty("getReportFormEndpoint")]
        public GetReportFormEndpoint GetReportFormEndpoint { get; set; }
    }

    public class MenuServiceItemRenderer
    {
        [JsonProperty("text")]
        public Text Text { get; set; }

        [JsonProperty("icon")]
        public Icon Icon { get; set; }

        [JsonProperty("serviceEndpoint")]
        public ServiceEndpoint ServiceEndpoint { get; set; }

        [JsonProperty("trackingParams")]
        public string TrackingParams { get; set; }
    }

    public class Item
    {
        [JsonProperty("menuNavigationItemRenderer")]
        public MenuNavigationItemRenderer MenuNavigationItemRenderer { get; set; }

        [JsonProperty("toggleMenuServiceItemRenderer")]
        public ToggleMenuServiceItemRenderer ToggleMenuServiceItemRenderer { get; set; }

        [JsonProperty("menuServiceItemRenderer")]
        public MenuServiceItemRenderer MenuServiceItemRenderer { get; set; }

        [JsonProperty("notificationTextRenderer")]
        public NotificationTextRenderer NotificationTextRenderer { get; set; }
    }

    public class MenuRenderer
    {
        [JsonProperty("items")]
        public List<Item> Items { get; set; }

        [JsonProperty("trackingParams")]
        public string TrackingParams { get; set; }
    }

    public class Menu
    {
        [JsonProperty("menuRenderer")]
        public MenuRenderer MenuRenderer { get; set; }
    }

    public class WatchEndpointMusicConfig
    {
        [JsonProperty("musicVideoType")]
        public string MusicVideoType { get; set; }
    }

    public class WatchEndpointMusicSupportedConfigs
    {
        [JsonProperty("watchEndpointMusicConfig")]
        public WatchEndpointMusicConfig WatchEndpointMusicConfig { get; set; }
    }

    public class DoubleTapCommand
    {
        [JsonProperty("clickTrackingParams")]
        public string ClickTrackingParams { get; set; }

        [JsonProperty("watchPlaylistEndpoint")]
        public WatchPlaylistEndpoint WatchPlaylistEndpoint { get; set; }

        [JsonProperty("watchEndpoint")]
        public WatchEndpoint WatchEndpoint { get; set; }
    }

    public class VerticalGradient
    {
        [JsonProperty("gradientLayerColors")]
        public List<string> GradientLayerColors { get; set; }
    }

    public class Background
    {
        [JsonProperty("verticalGradient")]
        public VerticalGradient VerticalGradient { get; set; }
    }

    public class PlayNavigationEndpoint
    {
        [JsonProperty("clickTrackingParams")]
        public string ClickTrackingParams { get; set; }

        [JsonProperty("watchEndpoint")]
        public WatchEndpoint WatchEndpoint { get; set; }

        [JsonProperty("watchPlaylistEndpoint")]
        public WatchPlaylistEndpoint WatchPlaylistEndpoint { get; set; }
    }

    public class AccessibilityDataLabel
    {
        [JsonProperty("label")]
        public string Label { get; set; }
    }

    public class AccessibilityDataXX
    {
        [JsonProperty("accessibilityData")]
        public AccessibilityDataLabel AccessibilityData { get; set; }
    }

    public class MusicPlayButtonRenderer
    {
        [JsonProperty("playNavigationEndpoint")]
        public PlayNavigationEndpoint PlayNavigationEndpoint { get; set; }

        [JsonProperty("trackingParams")]
        public string TrackingParams { get; set; }

        [JsonProperty("playIcon")]
        public Icon PlayIcon { get; set; }

        [JsonProperty("pauseIcon")]
        public Icon PauseIcon { get; set; }

        [JsonProperty("iconColor")]
        public object IconColor { get; set; }

        [JsonProperty("backgroundColor")]
        public int BackgroundColor { get; set; }

        [JsonProperty("activeBackgroundColor")]
        public int ActiveBackgroundColor { get; set; }

        [JsonProperty("loadingIndicatorColor")]
        public object LoadingIndicatorColor { get; set; }

        [JsonProperty("playingIcon")]
        public Icon PlayingIcon { get; set; }

        [JsonProperty("iconLoadingColor")]
        public int IconLoadingColor { get; set; }

        [JsonProperty("activeScaleFactor")]
        public int ActiveScaleFactor { get; set; }

        [JsonProperty("buttonSize")]
        public string ButtonSize { get; set; }

        [JsonProperty("rippleTarget")]
        public string RippleTarget { get; set; }

        [JsonProperty("accessibilityPlayData")]
        public AccessibilityDataXX AccessibilityPlayData { get; set; }

        [JsonProperty("accessibilityPauseData")]
        public AccessibilityDataXX AccessibilityPauseData { get; set; }
    }

    public class MusicItemThumbnailOverlayRenderer
    {
        [JsonProperty("background")]
        public Background Background { get; set; }

        [JsonProperty("content")]
        public Content Content { get; set; }

        [JsonProperty("contentPosition")]
        public string ContentPosition { get; set; }

        [JsonProperty("displayStyle")]
        public string DisplayStyle { get; set; }
    }

    public class Overlay
    {
        [JsonProperty("musicItemThumbnailOverlayRenderer")]
        public MusicItemThumbnailOverlayRenderer MusicItemThumbnailOverlayRenderer { get; set; }
    }

    public class MusicResponsiveListItemRenderer
    {
        [JsonProperty("trackingParams")]
        public string TrackingParams { get; set; }

        [JsonProperty("thumbnail")]
        public ThumbnailContainer Thumbnail { get; set; }

        [JsonProperty("flexColumns")]
        public List<FlexColumn> FlexColumns { get; set; }

        [JsonProperty("menu")]
        public Menu Menu { get; set; }

        [JsonProperty("flexColumnDisplayStyle")]
        public string FlexColumnDisplayStyle { get; set; }

        [JsonProperty("navigationEndpoint")]
        public NavigationEndpoint NavigationEndpoint { get; set; }

        [JsonProperty("itemHeight")]
        public string ItemHeight { get; set; }

        [JsonProperty("doubleTapCommand")]
        public DoubleTapCommand DoubleTapCommand { get; set; }

        [JsonProperty("overlay")]
        public Overlay Overlay { get; set; }
    }

    public class BottomText
    {
        [JsonProperty("runs")]
        public List<Run> Runs { get; set; }
    }

    public class BottomEndpoint
    {
        [JsonProperty("clickTrackingParams")]
        public string ClickTrackingParams { get; set; }

        [JsonProperty("searchEndpoint")]
        public SearchEndpoint SearchEndpoint { get; set; }
    }

    public class MusicShelfRenderer
    {
        [JsonProperty("title")]
        public Title Title { get; set; }

        [JsonProperty("contents")]
        public List<Content> Contents { get; set; }

        [JsonProperty("trackingParams")]
        public string TrackingParams { get; set; }

        [JsonProperty("bottomText")]
        public BottomText BottomText { get; set; }

        [JsonProperty("bottomEndpoint")]
        public BottomEndpoint BottomEndpoint { get; set; }
    }
    public class ReloadContinuationData
    {
        [JsonProperty("continuation")]
        public string Continuation { get; set; }

        [JsonProperty("clickTrackingParams")]
        public string ClickTrackingParams { get; set; }
    }

    public class Continuation
    {
        [JsonProperty("reloadContinuationData")]
        public ReloadContinuationData ReloadContinuationData { get; set; }
    }

    public class Style
    {
        [JsonProperty("styleType")]
        public string StyleType { get; set; }
    }

    public class ChipCloudChipRenderer
    {
        [JsonProperty("style")]
        public Style Style { get; set; }

        [JsonProperty("text")]
        public Text Text { get; set; }

        [JsonProperty("navigationEndpoint")]
        public NavigationEndpoint NavigationEndpoint { get; set; }

        [JsonProperty("trackingParams")]
        public string TrackingParams { get; set; }

        [JsonProperty("accessibilityData")]
        public AccessibilityDataXX AccessibilityData { get; set; }
    }

    public class Chip
    {
        [JsonProperty("chipCloudChipRenderer")]
        public ChipCloudChipRenderer ChipCloudChipRenderer { get; set; }
    }

    public class ChipCloudRenderer
    {
        [JsonProperty("chips")]
        public List<Chip> Chips { get; set; }

        [JsonProperty("collapsedRowCount")]
        public int CollapsedRowCount { get; set; }

        [JsonProperty("trackingParams")]
        public string TrackingParams { get; set; }

        [JsonProperty("horizontalScrollable")]
        public bool HorizontalScrollable { get; set; }
    }

    public class Header
    {
        [JsonProperty("chipCloudRenderer")]
        public ChipCloudRenderer ChipCloudRenderer { get; set; }
    }

    public class SectionListRenderer
    {
        [JsonProperty("contents")]
        public List<Content> Contents { get; set; }

        [JsonProperty("continuations")]
        public List<Continuation> Continuations { get; set; }

        [JsonProperty("trackingParams")]
        public string TrackingParams { get; set; }

        [JsonProperty("header")]
        public Header Header { get; set; }
    }

    public class Content
    {
        [JsonProperty("sectionListRenderer")]
        public SectionListRenderer SectionListRenderer { get; set; }

        [JsonProperty("musicPlayButtonRenderer")]
        public MusicPlayButtonRenderer MusicPlayButtonRenderer { get; set; }

        [JsonProperty("musicResponsiveListItemRenderer")]
        public MusicResponsiveListItemRenderer MusicResponsiveListItemRenderer { get; set; }

        [JsonProperty("musicShelfRenderer")]
        public MusicShelfRenderer MusicShelfRenderer { get; set; }
    }

    public class TabRenderer
    {
        [JsonProperty("endpoint")]
        public Endpoint Endpoint { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("selected")]
        public bool Selected { get; set; }

        [JsonProperty("content")]
        public Content Content { get; set; }

        [JsonProperty("tabIdentifier")]
        public string TabIdentifier { get; set; }

        [JsonProperty("trackingParams")]
        public string TrackingParams { get; set; }
    }

    public class Tab
    {
        [JsonProperty("tabRenderer")]
        public TabRenderer TabRenderer { get; set; }
    }

    public class TabbedSearchResultsRenderer
    {
        [JsonProperty("tabs")]
        public List<Tab> Tabs { get; set; }
    }

    public class Contents
    {
        [JsonProperty("tabbedSearchResultsRenderer")]
        public TabbedSearchResultsRenderer TabbedSearchResultsRenderer { get; set; }

        [JsonProperty("sectionListRenderer")]
        public SectionListRenderer SectionListRenderer { get; set; }
    }

    public class AdSafetyReason
    {
    }


    public class GeneratedSearchResult
    {
        [JsonProperty("responseContext")]
        public ResponseContext ResponseContext { get; set; }

        [JsonProperty("estimatedResults")]
        public string EstimatedResults { get; set; }

        [JsonProperty("trackingParams")]
        public string TrackingParams { get; set; }

        [JsonProperty("contents")]
        public Contents Contents { get; set; }

        [JsonProperty("adSafetyReason")]
        public AdSafetyReason AdSafetyReason { get; set; }
    }


}
