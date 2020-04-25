<template>
    <div>
        <h1>Filmy wg aktorów</h1>
        <ol class="categories" v-for="cast in wszyscyAktorzy" v-bind:key="cast.id">
            <h3>{{ cast }}</h3>
            <template v-for="movie in ostatecznieWszytskieFilmyZAktorami">
				<li v-bind:key="movie.id1" v-if="movie.cast == cast">
					{{ movie.title }}
				</li>
			</template>
        </ol>
        <ol class="categories">
            <h3>NoActors</h3>
            <template v-for="movie in ostatecznieWszytskieFilmyZAktorami">
				<li v-bind:key="movie.id2" v-if="movie.cast == ''">
					{{ movie.title }}
				</li>
			</template>
        </ol>
    </div>
</template>

<script>
var _ = require('underscore'); 

export default {
    name: "ListOfCasts",
    props: ["hundredMoviesActors"],
    data() {
        return {
            wszyscyAktorzy: null,
            ostatecznieWszytskieFilmyZAktorami: null
        };
    },
    mounted() {
        function onlyUnique(value, index, self) { 
            return self.indexOf(value) === index;
        }
        let tab = _.map(this.hundredMoviesActors, 'cast')
        let newTab = _.flatten(tab)
        let lastTabOfCasts = newTab.filter(onlyUnique)
        // window.console.log(tab)
        // window.console.log(newTab)
        // window.console.log(lastTabOfCasts)
        this.wszyscyAktorzy = lastTabOfCasts;

        let nextID = 0;
        let tablicaRozlozonychAktorow = _.each(this.hundredMoviesActors, (item) => {
            item.id = nextID;
            if (item.cast.length > 1) {
                let casts = [...item.cast];
                item.cast = [casts[0]];
                
                const limit = casts.length;
                for (let i = 1; i < limit; i++) {
                    let newItem = {
                        id: ++nextID,
                        title: item.title,
                        year: item.year,
                        genres: item.genres,
                        cast: [casts[i]]                        
                    }
                    this.hundredMoviesActors.push(newItem);
                }
            }
            nextID++;
        });
        this.ostatecznieWszytskieFilmyZAktorami = tablicaRozlozonychAktorow;
    }
}
</script>

<style scoped>

</style>