Test

Test
<
series:
 {
    show|
    \<h2>Show: <show.SeriesName> \</h2>
	\<ul>
    <
    show.Seasons.values:
     {
      season|
      \<li>Season <season.SeasonNumber> $season.SeasonNumber$ <season.PosterUrl>\</li>
	  \<ul>
		<
		season.Episodes:
		 {
		  episode|
		  \<li>Episode <episode.EpisodeNumber>: <episode.EpisodeName>\</li>
		 }
		>
		\</ul>
     }
    >
	\</ul>
 }
>