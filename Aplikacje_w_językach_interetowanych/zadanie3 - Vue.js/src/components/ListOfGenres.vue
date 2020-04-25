<template>
    <div>
        <h1>Filmy wg gatunku</h1>
        <ol class="categories" v-for="genre in wszystkieRodzaje" v-bind:key="genre.id">
            <h3>{{ genre }}</h3>
            <template v-for="movie in ostatecznieWszytskieFilmyZRodzajami">
				<li v-bind:key="movie.id3" v-if="movie.genres == genre">
					{{ movie.title }}
				</li>
			</template>
        </ol>
        <ol class="categories">
            <h3>NoCategory</h3>
            <template v-for="movie in ostatecznieWszytskieFilmyZRodzajami">
				<li v-bind:key="movie.id4" v-if="movie.genres == ''">
					{{ movie.title }}
				</li>
			</template>
        </ol>
    </div>
</template>

<script>
var _ = require('underscore'); 

export default {
    name: "ListOfGenres",
    props: ["hundredMovies"],
    data() {
        return {
            wszystkieRodzaje: null,
            ostatecznieWszytskieFilmyZRodzajami: null
        };
    },
    mounted() {
        function onlyUnique(value, index, self) { 
            return self.indexOf(value) === index;
        }
        let tab = _.map(this.hundredMovies, 'genres')
        let newTab = _.flatten(tab)
        let lastTabOfRodzajeXD = newTab.filter(onlyUnique)
        // window.console.log(tab)
        // window.console.log(newTab)
        // window.console.log(lastTabOfRodzajeXD)
        this.wszystkieRodzaje = lastTabOfRodzajeXD;

        let nextID = 0;
        let tablicaRozlozonychRodzajow = _.each(this.hundredMovies, (item) => {
            item.id = nextID;
            if (item.genres.length > 1) {
                let genres = [...item.genres];
                item.genres = [genres[0]];
                const limit = genres.length;
                for (let i = 1; i < limit; i++) {
                    let newItem = {
                        id: ++nextID,
                        title: item.title,
                        year: item.year,
                        cast: item.cast,
                        genres: [genres[i]]
                    }
                    this.hundredMovies.push(newItem);
                }
            }
            nextID++;
        });
        this.ostatecznieWszytskieFilmyZRodzajami = tablicaRozlozonychRodzajow;
    }
}
</script>

<style scoped>

</style>