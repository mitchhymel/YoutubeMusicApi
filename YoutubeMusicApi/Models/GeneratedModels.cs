using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace YoutubeMusicApi.Models.Generated
{
    /// <summary>
    /// Models generated from json responses from YTM endpoints
    /// with some manual changes to simplify things
    /// </summary>

    public class BrowseResponse
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

        [JsonProperty("continuationContents")]
        public ContinuationContents ContinuationContents { get; set; }

        [JsonProperty("header")]
        public Header Header { get; set; }

        [JsonProperty("playlistId")]
        public string PlaylistId { get; set; }

        [JsonProperty("command")]
        public Command Command { get; set; }
    }
    public class Command
    {
        [JsonProperty("clickTrackingParams")]
        public string ClickTrackingParams { get; set; }

        [JsonProperty("handlePlaylistDeletionCommand")]
        public HandlePlaylistDeletionCommand HandlePlaylistDeletionCommand { get; set; }
    }

    public class HandlePlaylistDeletionCommand
    {
        [JsonProperty("playlistId")]
        public string PlaylistId { get; set; }
    }

    public class ContinuationContents
    {
        [JsonProperty("gridContinuation")]
        public SectionListRenderer GridContinuation { get; set; }
    }

    public class Contents
    {
        [JsonProperty("tabbedSearchResultsRenderer")]
        public TabbedSearchResultsRenderer TabbedSearchResultsRenderer { get; set; }

        [JsonProperty("sectionListRenderer")]
        public SectionListRenderer SectionListRenderer { get; set; }

        [JsonProperty("singleColumnBrowseResultsRenderer")]
        public TabbedSearchResultsRenderer SingleColumnBrowseResultsRenderer { get; set; }
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

        [JsonProperty("musicCarouselShelfRenderer")]
        public MusicCarouselShelfRenderer MusicCarouselShelfRenderer { get; set; }

        [JsonProperty("musicTwoRowItemRenderer")]
        public MusicTwoRowItemRenderer MusicTwoRowItemRenderer { get; set; }

        [JsonProperty("itemSectionRenderer")]
        public SectionListRenderer ItemSectionRenderer;

        [JsonProperty("gridRenderer")]
        public SectionListRenderer GridRenderer;

        [JsonProperty("musicPlaylistShelfRenderer")]
        public MusicPlaylistShelfRenderer MusicPlaylistShelfRenderer { get; set; }
    }

    public class MusicPlaylistShelfRenderer
    { 
        [JsonProperty("collapsedItemCount")]
        public int CollapsedItemCount { get; set; }

        [JsonProperty("contents")]
        public List<Content> Contents;

        [JsonProperty("contentsReorderable")]
        public bool ContentsReorderable { get; set; }

        [JsonProperty("playlistId")]
        public string PlaylistId { get; set; }

        [JsonProperty("trackingParams")]
        public string TrackingParams { get; set; }

        [JsonProperty("continuations")]
        public List<Continuation> Continuations { get; set; }

    }


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

        [JsonProperty("croppedSquareThumbnailRenderer")]
        public MusicThumbnailRenderer CroppedSquareThumbnailRenderer { get; set; }
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

    public class MusicResponsiveListItemFlexColumnRenderer
    {
        [JsonProperty("text")]
        public RunsList Text { get; set; }

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

    public class CreatePlaylistEndpoint
    {
        [JsonProperty("hack")]
        public bool Hack { get; set; }
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

        [JsonProperty("createPlaylistEndpoint")]
        public CreatePlaylistEndpoint CreatePlaylistEndpoint { get; set; }
    }

    public class MenuNavigationItemRenderer
    {
        [JsonProperty("text")]
        public RunsList Text { get; set; }

        [JsonProperty("icon")]
        public Icon Icon { get; set; }

        [JsonProperty("navigationEndpoint")]
        public NavigationEndpoint NavigationEndpoint { get; set; }

        [JsonProperty("trackingParams")]
        public string TrackingParams { get; set; }
    }

    public class RunsList
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

    public class ToggledIcon
    {
        [JsonProperty("iconType")]
        public string IconType { get; set; }
    }

    public class ToggleMenuServiceItemRenderer
    {
        [JsonProperty("defaultText")]
        public RunsList DefaultText { get; set; }

        [JsonProperty("defaultIcon")]
        public DefaultIcon DefaultIcon { get; set; }

        [JsonProperty("defaultServiceEndpoint")]
        public ServiceEndpoint DefaultServiceEndpoint { get; set; }

        [JsonProperty("toggledText")]
        public RunsList ToggledText { get; set; }

        [JsonProperty("toggledIcon")]
        public ToggledIcon ToggledIcon { get; set; }

        [JsonProperty("toggledServiceEndpoint")]
        public ServiceEndpoint ToggledServiceEndpoint { get; set; }

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


    public class NotificationTextRenderer
    {
        [JsonProperty("successResponseText")]
        public RunsList SuccessResponseText { get; set; }

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

        [JsonProperty("likeEndpoint")]
        public LikeEndpoint LikeEndpoint { get; set; }

        [JsonProperty("feedbackEndpoint")]
        public FeedbackEndpoint FeedbackEndpoint { get; set; }
    }

    public class MenuServiceItemRenderer
    {
        [JsonProperty("text")]
        public RunsList Text { get; set; }

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

        [JsonProperty("accessibility")]
        public AccessibilityDataContainer Accessibility { get; set; }

        [JsonProperty("topLevelButtons")]
        public List<TopLevelButton> TopLevelButtons { get; set; }
    }
    public class TopLevelButton
    {
        [JsonProperty("likeButtonRenderer")]
        public LikeButtonRenderer LikeButtonRenderer { get; set; }
    }
    public class LikeButtonRenderer
    {
        [JsonProperty("target")]
        public Target Target { get; set; }

        [JsonProperty("likeStatus")]
        public string LikeStatus { get; set; }

        [JsonProperty("trackingParams")]
        public string TrackingParams { get; set; }

        [JsonProperty("likesAllowed")]
        public bool LikesAllowed { get; set; }

        [JsonProperty("serviceEndpoints")]
        public List<ServiceEndpoint> ServiceEndpoints { get; set; }
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

    public class AccessibilityDataContainer
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
        public string IconColor { get; set; }

        [JsonProperty("backgroundColor")]
        public string BackgroundColor { get; set; }

        [JsonProperty("activeBackgroundColor")]
        public string ActiveBackgroundColor { get; set; }

        [JsonProperty("loadingIndicatorColor")]
        public string LoadingIndicatorColor { get; set; }

        [JsonProperty("playingIcon")]
        public Icon PlayingIcon { get; set; }

        [JsonProperty("iconLoadingColor")]
        public string IconLoadingColor { get; set; }

        [JsonProperty("activeScaleFactor")]
        public double ActiveScaleFactor { get; set; }

        [JsonProperty("buttonSize")]
        public string ButtonSize { get; set; }

        [JsonProperty("rippleTarget")]
        public string RippleTarget { get; set; }

        [JsonProperty("accessibilityPlayData")]
        public AccessibilityDataContainer AccessibilityPlayData { get; set; }

        [JsonProperty("accessibilityPauseData")]
        public AccessibilityDataContainer AccessibilityPauseData { get; set; }
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

        [JsonProperty("fixedColumns")]
        public List<FixedColumn> FixedColumns { get; set; }

        [JsonProperty("playlistItemData")]
        public PlaylistItemData PlaylistItemData { get; set; }
    }

    public class MusicResponsiveListItemFixedColumnRenderer
    {
        [JsonProperty("text")]
        public RunsList Text { get; set; }

        [JsonProperty("displayPriority")]
        public string DisplayPriority { get; set; }

        [JsonProperty("size")]
        public string Size { get; set; }
    }

    public class FixedColumn
    {
        [JsonProperty("musicResponsiveListItemFixedColumnRenderer")]
        public MusicResponsiveListItemFixedColumnRenderer MusicResponsiveListItemFixedColumnRenderer { get; set; }
    }

    public class PlaylistItemData
    { 
        [JsonProperty("playlistSetVideoId")]
        public string PlaylistSetVideoId { get; set; }
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
        public RunsList Title { get; set; }

        [JsonProperty("contents")]
        public List<Content> Contents { get; set; }

        [JsonProperty("trackingParams")]
        public string TrackingParams { get; set; }

        [JsonProperty("bottomText")]
        public RunsList BottomText { get; set; }

        [JsonProperty("bottomEndpoint")]
        public BottomEndpoint BottomEndpoint { get; set; }
    }
    public class ContinuationData
    {
        [JsonProperty("continuation")]
        public string Continuation { get; set; }

        [JsonProperty("clickTrackingParams")]
        public string ClickTrackingParams { get; set; }
    }

    public class Continuation
    {
        [JsonProperty("reloadContinuationData")]
        public ContinuationData ReloadContinuationData { get; set; }

        [JsonProperty("nextContinuationData")]
        public ContinuationData NextContinuationData { get; set; }
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
        public RunsList Text { get; set; }

        [JsonProperty("navigationEndpoint")]
        public NavigationEndpoint NavigationEndpoint { get; set; }

        [JsonProperty("trackingParams")]
        public string TrackingParams { get; set; }

        [JsonProperty("accessibilityData")]
        public AccessibilityDataContainer AccessibilityData { get; set; }
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

        [JsonProperty("musicVisualHeaderRenderer")]
        public MusicVisualHeaderRenderer MusicVisualHeaderRenderer { get; set; }

        [JsonProperty("musicCarouselShelfBasicHeaderRenderer")]
        public MusicCarouselShelfBasicHeaderRenderer MusicCarouselShelfBasicHeaderRenderer { get; set; }

        [JsonProperty("musicEditablePlaylistDetailHeaderRenderer")]
        public MusicEditablePlaylistDetailHeaderRenderer MusicEditablePlaylistDetailHeaderRenderer { get; set; }

        [JsonProperty("musicDetailHeaderRenderer")]
        public MusicDetailHeaderRenderer MusicDetailHeaderRenderer { get; set; }

    }

    public class MusicDetailHeaderRenderer
    {
        [JsonProperty("title")]
        public RunsList Title { get; set; }

        [JsonProperty("subtitle")]
        public RunsList Subtitle { get; set; }

        [JsonProperty("menu")]
        public Menu Menu { get; set; }

        [JsonProperty("thumbnail")]
        public ThumbnailContainer Thumbnail { get; set; }

        [JsonProperty("trackingParams")]
        public string TrackingParams { get; set; }

        [JsonProperty("moreButton")]
        public MoreButton MoreButton { get; set; }

        [JsonProperty("secondSubtitle")]
        public RunsList SecondSubtitle { get; set; }

        [JsonProperty("privacy")]
        public string Privacy { get; set; }
    }

    public class MoreButton
    {
        [JsonProperty("toggleButtonRenderer")]
        public ToggleButtonRenderer ToggleButtonRenderer { get; set; }
    }

    public class ToggleButtonRenderer
    {
        [JsonProperty("isToggled")]
        public bool IsToggled { get; set; }

        [JsonProperty("isDisabled")]
        public bool IsDisabled { get; set; }

        [JsonProperty("defaultIcon")]
        public Icon DefaultIcon { get; set; }

        [JsonProperty("defaultText")]
        public RunsList DefaultText { get; set; }

        [JsonProperty("toggledIcon")]
        public Icon ToggledIcon { get; set; }

        [JsonProperty("toggledText")]
        public RunsList ToggledText { get; set; }

        [JsonProperty("trackingParams")]
        public string TrackingParams { get; set; }
    }

    public class MusicEditablePlaylistDetailHeaderRenderer
    {
        [JsonProperty("header")]
        public Header Header { get; set; }

        [JsonProperty("editHeader")]
        public EditHeader EditHeader { get; set; }

        [JsonProperty("trackingParams")]
        public string TrackingParams { get; set; }

        [JsonProperty("playlistId")]
        public string PlaylistId { get; set; }
    }

    public class EditHeader
    {
        [JsonProperty("musicPlaylistEditHeaderRenderer")]
        public MusicPlaylistEditHeaderRenderer MusicPlaylistEditHeaderRenderer { get; set; }
    }

    public class MusicPlaylistEditHeaderRenderer
    {
        [JsonProperty("title")]
        public RunsList Title { get; set; }

        [JsonProperty("editTitle")]
        public RunsList EditTitle { get; set; }

        [JsonProperty("editDescription")]
        public RunsList EditDescription { get; set; }

        [JsonProperty("privacy")]
        public string Privacy { get; set; }

        [JsonProperty("trackingParams")]
        public string TrackingParams { get; set; }

        [JsonProperty("playlistId")]
        public string PlaylistId { get; set; }

        [JsonProperty("collaborationSettingsCommand")]
        public CollaborationSettingsCommand CollaborationSettingsCommand { get; set; }
    }

    public class CollaborationSettingsCommand
    {
        [JsonProperty("clickTrackingParams")]
        public string ClickTrackingParams { get; set; }

        [JsonProperty("playlistEditorEndpoint")]
        public PlaylistEditorEndpoint PlaylistEditorEndpoint { get; set; }
    }

    public class PlaylistEditorEndpoint
    {
        [JsonProperty("playlistId")]
        public string PlaylistId { get; set; }

        [JsonProperty("openCollaborationPage")]
        public bool OpenCollaborationPage { get; set; }
    }

    public class MusicVisualHeaderRenderer
    {
        [JsonProperty("title")]
        public RunsList Title { get; set; }

        [JsonProperty("trackingParams")]
        public string TrackingParams { get; set; }

        [JsonProperty("menu")]
        public Menu Menu { get; set; }

        [JsonProperty("foregroundThumbnail")]
        public MusicThumbnailRenderer ForegroundThumbnail { get; set; }
    }

    public class MusicCarouselShelfBasicHeaderRenderer
    {
        [JsonProperty("title")]
        public RunsList Title { get; set; }

        [JsonProperty("accessibilityData")]
        public AccessibilityDataContainer AccessibilityData { get; set; }

        [JsonProperty("headerStyle")]
        public string HeaderStyle { get; set; }

        [JsonProperty("trackingParams")]
        public string TrackingParams { get; set; }

        [JsonProperty("endIcons")]
        public List<EndIcon> EndIcons { get; set; }
    }

    public class IconLinkRenderer
    {
        [JsonProperty("icon")]
        public Icon Icon { get; set; }

        [JsonProperty("tooltip")]
        public RunsList Tooltip { get; set; }

        [JsonProperty("navigationEndpoint")]
        public NavigationEndpoint NavigationEndpoint { get; set; }

        [JsonProperty("trackingParams")]
        public string TrackingParams { get; set; }
    }

    public class EndIcon
    {
        [JsonProperty("iconLinkRenderer")]
        public IconLinkRenderer IconLinkRenderer { get; set; }
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

        [JsonProperty("isCollapsible")]
        public bool IsCollapsible { get; set; }

        [JsonProperty("itemSize")]
        public string ItemSize { get; set; }

        [JsonProperty("items")]
        public List<Content> Items { get; set; }
    }

    public class MusicTwoRowItemRenderer
    {
        [JsonProperty("thumbnailRenderer")]
        public ThumbnailContainer ThumbnailRenderer { get; set; }

        [JsonProperty("aspectRatio")]
        public string AspectRatio { get; set; }

        [JsonProperty("title")]
        public RunsList Title { get; set; }

        [JsonProperty("subtitle")]
        public RunsList Subtitle { get; set; }

        [JsonProperty("navigationEndpoint")]
        public NavigationEndpoint NavigationEndpoint { get; set; }

        [JsonProperty("trackingParams")]
        public string TrackingParams { get; set; }

        [JsonProperty("accessibilityData")]
        public AccessibilityDataContainer AccessibilityData { get; set; }

        [JsonProperty("menu")]
        public Menu Menu { get; set; }

        [JsonProperty("thumbnailOverlay")]
        public Overlay ThumbnailOverlay { get; set; }
    }

    public class MusicCarouselShelfRenderer
    {
        [JsonProperty("header")]
        public Header Header { get; set; }

        [JsonProperty("contents")]
        public List<Content> Contents { get; set; }

        [JsonProperty("trackingParams")]
        public string TrackingParams { get; set; }

        [JsonProperty("itemSize")]
        public string ItemSize { get; set; }

        [JsonProperty("backgroundOverlay")]
        public Background BackgroundOverlay { get; set; }
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

    public class AdSafetyReason
    {
    }


}
