<template>
    <div class="container">
        <h1> FILM LIST </h1>
        <table>
            <thead>
                <tr>
                    <th>Title</th>
                    <th>Production Year</th>
                    <th>Cast</th>
                    <th>Genres</th>
                </tr>
            </thead>
            <tbody>
                <tr v-bind:key="movie.id" v-for="movie in filteredFilms">
                    <td>{{movie.title}}</td>
                    <td>{{movie.year}}</td>
                    <td>{{movie.cast.toString()}}</td>
                    <td>{{movie.genres.toString()}}</td>
                </tr>
            </tbody>
        </table>    
    </div>
</template>

<script>
export default {
    name: "TableWithFilms",
    props: ["filteredFilms"]
}
</script>

<style scoped>

</style>