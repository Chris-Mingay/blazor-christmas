<script context="module">
	export function load({error, status}){
		return {
			props:{
				error,
				status
			}
		}
	}
</script>

<script>
	export let status
	export let error
</script>

<div class='bg-red-700 text-white rounded-2xl overflow-hidden p-4 mx-auto shadow-xl'>
	<h1 class='font-bold text-4xl mb-2 text-red-300'>{status}</h1>
	<p>{error.message}</p>
</div>
