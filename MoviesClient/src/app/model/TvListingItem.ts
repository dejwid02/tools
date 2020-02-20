import { Movie } from './Movie';
import { Channel } from './Channel';

export interface TvListingItem {
    id: number;
    movie: Movie;
    channel: Channel;
    startTime: Date;
}